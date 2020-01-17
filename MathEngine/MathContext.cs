using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Fasterflect.Extensions;
using ExtraUtils.MathEngine.Functions;

namespace ExtraUtils.MathEngine
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IMathContext"/> interface.
    /// </summary>
    /// <seealso cref="IMathContext" />
    public sealed class MathContext : IMathContext
    {
        /// <summary>
        /// Gets the default <see cref="IMathContext"/>.
        /// </summary>
        /// <value>
        /// The default context.
        /// </value>
        public static MathContext Default { get; } = new MathContext();

        /// <summary>
        /// Gets all the functions of this context.
        /// </summary>
        /// <value>
        /// The functions.
        /// </value>
        public IReadOnlyDictionary<string, IFunction> Functions { get; }
        /// <summary>
        /// Gets all the binary operators of this context.
        /// </summary>
        /// <value>
        /// The binary operators.
        /// </value>
        public IReadOnlyDictionary<string, IBinaryOperator> BinaryOperators { get; }
        /// <summary>
        /// Gets all the unary operators of this context.
        /// </summary>
        /// <value>
        /// The unary operators.
        /// </value>
        public IReadOnlyDictionary<string, IUnaryOperator> UnaryOperators { get; }
        /// <summary>
        /// Gets all the constants and variables of this context.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public IReadOnlyDictionary<string, double> Values { get; }

        private MathContext()
        {
            Functions = GetMathContextFunctions();
            BinaryOperators = GetMathContextDataOfType<IBinaryOperator>();
            UnaryOperators = GetMathContextDataOfType<IUnaryOperator>();
            Values = GetMathContextValues();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MathContext"/> class.
        /// </summary>
        /// <param name="values">The values to use in this context.</param>
        public MathContext(params (string, double)[] values)
        {
            Functions = Default.Functions;
            BinaryOperators = Default.BinaryOperators;
            UnaryOperators = Default.UnaryOperators;
            Values = GetMathContextValues(values);
        }

        private static IReadOnlyDictionary<string, double> GetMathContextValues(params (string, double)[] variables)
        {
            var builder = ImmutableDictionary.CreateBuilder<string, double>(StringIgnoreCaseEqualityComparer.Instance);
            builder.Add("pi", Math.PI);
            builder.Add("e", Math.E);
            builder.Add("infinity", double.PositiveInfinity);

            if(variables.Length > 0)
            {
                foreach(var e in variables)
                {
                    builder.Add(e.Item1, e.Item2);
                }
            }

            return builder.ToImmutable();
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

        public bool IsFunction(string functionName) => Functions.ContainsKey(functionName);

        public bool IsBinaryOperator(string symbol) => BinaryOperators.ContainsKey(symbol);

        public bool IsUnaryOperator(string symbol) => UnaryOperators.ContainsKey(symbol);

        public bool IsVariableOrConstant(string name) => Values.ContainsKey(name);

        public bool TryGetFunction(string functionName, [NotNullWhen(true)] out IFunction? func)
        {
            return Functions.TryGetValue(functionName, out func);
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
            return BinaryOperators.TryGetValue(symbol, out op);
        }

        public bool TryGetUnaryOperator(string symbol, [NotNullWhen(true)] out IUnaryOperator? op)
        {
            return UnaryOperators.TryGetValue(symbol, out op);
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

        public double GetVariableOrConstant(string name)
        {
            if (Values.TryGetValue(name, out var result))
            {
                return result;
            }

            throw new Exception($"Cannot find the value named: {name}");
        }
    }
}
