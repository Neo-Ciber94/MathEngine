using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Fasterflect.Extensions;
using MathEngine.Functions;

namespace MathEngine
{
    public class MathContext : IMathContext
    {
        public static MathContext Default { get; }

        static MathContext()
        {
            Default = new MathContext();
        }

        private readonly IReadOnlyDictionary<string, IFunction> _functions;
        private readonly IReadOnlyDictionary<string, IBinaryOperator> _binaryOperators;
        private readonly IReadOnlyDictionary<string, IUnaryOperator> _unaryOperators;
        private readonly IReadOnlyDictionary<string, double> _values;

        private MathContext()
        {
            _functions = GetMathContextFunctions();
            _binaryOperators = GetMathContextDataOfType<IBinaryOperator>();
            _unaryOperators = GetMathContextDataOfType<IUnaryOperator>();
            _values = GetConstantsAndVariables();
        }

        public MathContext(params (string, double)[] variables)
        {
            var defaultInstance = MathContext.Default;
            _functions = defaultInstance._functions;
            _binaryOperators = defaultInstance._binaryOperators;
            _unaryOperators = defaultInstance._unaryOperators;

            var dictionary = GetConstantsAndVariables() as Dictionary<string, double>;
            foreach (var e in variables)
            {
                dictionary!.Add(e.Item1, e.Item2);
            }

            _values = dictionary!;
        }

        private static IReadOnlyDictionary<string, double> GetConstantsAndVariables()
        {
            return new Dictionary<string, double>(StringIgnoreCaseEqualityComparer.Instance)
            {
                { "pi", Math.PI },
                { "e", Math.E },
                { "infinity", double.PositiveInfinity }
            };
        }

        private static IReadOnlyDictionary<string, IFunction> GetMathContextFunctions()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .TypesImplementing<IFunction>()
                .Concat(Assembly.GetEntryAssembly().TypesImplementing<IFunction>());

            return types.Where(t =>
            {
                var interfaces = t.GetInterfaces();
                return interfaces.Where(s => typeof(IFunction).IsAssignableFrom(s)).Count() == 1 || interfaces.Where(s => typeof(IInfixFunction).IsAssignableFrom(s)).Count() == 1;
            })
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (IFunction)Activator.CreateInstance(t)!)
                .ToImmutableDictionary((f) => f!.Name, (f) => f, StringIgnoreCaseEqualityComparer.Instance)!;
        }

        private static IReadOnlyDictionary<string, T> GetMathContextDataOfType<T>() where T : IFunction
        {
            return GetAllTypesThatDirectlyImplements<T>()
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                .Select(t => (T)Activator.CreateInstance(t)!)
                .ToImmutableDictionary((f) => f!.Name, (f) => f, StringIgnoreCaseEqualityComparer.Instance)!;
        }

        private static IEnumerable<Type> GetAllTypesThatDirectlyImplements<T>()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .TypesImplementing<T>()
                .Concat(Assembly.GetEntryAssembly().TypesImplementing<T>());

            return types.Where(t => t.GetInterfaces().Where(s => typeof(T).IsAssignableFrom(s)).Count() == 1);
        }

        public bool IsFunction(string functionName) => _functions.ContainsKey(functionName);

        public bool IsBinaryOperator(string symbol) => _binaryOperators.ContainsKey(symbol);

        public bool IsUnaryOperator(string symbol) => _unaryOperators.ContainsKey(symbol);

        public bool IsVariableOrConstant(string name) => _values.ContainsKey(name);

        public bool TryGetFunction(string functionName, [NotNullWhen(true)] out IFunction? func)
        {
            return _functions.TryGetValue(functionName, out func);
        }

        public bool TryGetInfixFunction(string functionName, [NotNullWhen(true)] out IInfixFunction? func)
        {
            func = null;
            if (TryGetFunction(functionName, out var outFunc))
            {
                func = outFunc as IInfixFunction;
                return func != null;
            }

            return false;
        }

        public bool TryGetBinaryOperator(string symbol, [NotNullWhen(true)] out IBinaryOperator? op)
        {
            return _binaryOperators.TryGetValue(symbol, out op);
        }

        public bool TryGetUnaryOperator(string symbol, [NotNullWhen(true)] out IUnaryOperator? op)
        {
            return _unaryOperators.TryGetValue(symbol, out op);
        }

        public IFunction GetFunction(string functionName)
        {
            if (TryGetFunction(functionName, out var func))
            {
                return func;
            }

            throw new Exception($"Cannot find the function named: {functionName}");
        }

        public IInfixFunction GetInfixFunction(string functionName)
        {
            if (TryGetFunction(functionName, out var func) && func is IInfixFunction)
            {
                return (IInfixFunction)func;
            }

            throw new Exception($"Cannot find the function named: {functionName}");
        }

        public IBinaryOperator GetBinaryOperator(string symbol)
        {
            if (TryGetBinaryOperator(symbol, out var op))
            {
                return op;
            }

            throw new Exception($"Cannot find the operator named: {symbol}");
        }

        public IUnaryOperator GetUnaryOperator(string symbol)
        {
            if (TryGetUnaryOperator(symbol, out var op))
            {
                return op;
            }

            throw new Exception($"Cannot find the operator named: {symbol}");
        }

        public double GetValue(string name)
        {
            if (_values.TryGetValue(name, out var result))
            {
                return result;
            }

            throw new Exception($"Cannot find the value named: {name}");
        }
    }
}
