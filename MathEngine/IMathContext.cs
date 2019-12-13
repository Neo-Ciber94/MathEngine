using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using MathEngine.Functions;

namespace MathEngine
{
    public interface IMathContext
    {
        private static IMathContext _default;

        public static IMathContext Default
        {
            get
            {
                if(_default == null)
                {
                    _default = new DefaultMathContext();
                }

                return _default;
            }
        }

        public bool IsFunction(string functionName);
        public bool IsBinaryOperator(string symbol);
        public bool IsUnaryOperator(string symbol);
        public bool IsVariableOrConstant(string name);
        public bool IsBinaryOperator(char symbol) => IsBinaryOperator(symbol.ToString());
        public bool IsUnaryOperator(char symbol) => IsUnaryOperator(symbol.ToString());
        public bool TryGetFunction(string functionName, [NotNullWhen(returnValue: true)] out IFunction? func);
        public bool TryGetBinaryOperator(string symbol, [NotNullWhen(returnValue: true)] out IBinaryOperator? op);
        public bool TryGetUnaryOperator(string symbol, [NotNullWhen(returnValue: true)]  out IUnaryOperator? op);
        public IFunction GetFunction(string functionName)
        {
            if (TryGetFunction(functionName, out var func))
            {
                return func!;
            }

            throw new Exception($"Cannot find the specified function: {functionName}.");
        }
        public IBinaryOperator GetBinaryOperator(string symbol)
        {
            if (TryGetBinaryOperator(symbol, out var op))
            {
                return op!;
            }

            throw new Exception($"Cannot find the specified operator: {symbol}.");
        }
        public IUnaryOperator GetUnaryOperator(string symbol)
        {
            if (TryGetUnaryOperator(symbol, out var op))
            {
                return op!;
            }

            throw new Exception($"Cannot find the specified operator: {symbol}.");
        }
        public double GetValue(string name);
    }

    class DefaultMathContext : IMathContext
    {
        private readonly IReadOnlyDictionary<string, IBinaryOperator> _binaryOperators;
        private readonly IReadOnlyDictionary<string, IUnaryOperator> _unaryOperators;
        private readonly IReadOnlyDictionary<string, IFunction> _functions;
        private readonly IReadOnlyDictionary<string, double> _variables;

        public DefaultMathContext()
        {
            _binaryOperators = GetBinaryOperators();
            _unaryOperators = GetUnaryOperators();
            _functions = GetFunctions();
            _variables = GetVariablesAndConstants();
        }

        private IReadOnlyDictionary<string, IBinaryOperator> GetBinaryOperators()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, IBinaryOperator>(StringIgnoreCaseEqualityComparer.Instance);
            builder.Add("+", new BinaryOperator('+', precedence: 0, OperatorAssociativity.Left, (a, b) => (a + b)));
            builder.Add("-", new BinaryOperator('-', precedence: 0, OperatorAssociativity.Left, (a, b) => (a - b)));
            builder.Add("*", new BinaryOperator('*', precedence: 1, OperatorAssociativity.Left, (a, b) => (a * b)));
            builder.Add("/", new BinaryOperator('/', precedence: 1, OperatorAssociativity.Left, (a, b) => (a / b)));
            builder.Add("^", new BinaryOperator('^', precedence: 2, OperatorAssociativity.Right, (a, b) => Math.Pow(a, b)));
            return builder.ToImmutable();
        }

        private IReadOnlyDictionary<string, IUnaryOperator> GetUnaryOperators()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, IUnaryOperator>(StringIgnoreCaseEqualityComparer.Instance);
            builder.Add("+", new UnaryOperator('+', OperatorNotation.Prefix, (a) => a));
            builder.Add("-", new UnaryOperator('-', OperatorNotation.Prefix, (a) => -a));
            return builder.ToImmutable();
        }

        private IReadOnlyDictionary<string, IFunction> GetFunctions()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, IFunction>(StringIgnoreCaseEqualityComparer.Instance);
            builder.Add("max", new Function("max", arity: 2, (args) => Math.Max(args[0], args[1])));
            builder.Add("min", new Function("min", arity: 2, (args) => Math.Min(args[0], args[1])));
            builder.Add("mod", new Function("mod", arity: 2, (args) => args[0] % args[1]));
            return builder.ToImmutable();
        }

        private IReadOnlyDictionary<string, double> GetVariablesAndConstants()
        {
            var builder = ImmutableDictionary.CreateBuilder<string, double>(StringIgnoreCaseEqualityComparer.Instance);
            builder.Add("pi", Math.PI);
            builder.Add("e", Math.E);
            return builder.ToImmutable();
        }

        public bool IsFunction(string functionName) => _functions.ContainsKey(functionName);

        public bool IsBinaryOperator(string symbol) => _binaryOperators.ContainsKey(symbol);

        public bool IsUnaryOperator(string symbol) => _unaryOperators.ContainsKey(symbol);

        public bool IsVariableOrConstant(string name) => _variables.ContainsKey(name);

        public bool TryGetFunction(string functionName, [NotNullWhen(true)] out IFunction? func)
        {
            return _functions.TryGetValue(functionName, out func);
        }

        public bool TryGetBinaryOperator(string symbol, [NotNullWhen(true)] out IBinaryOperator? op)
        {
            return _binaryOperators.TryGetValue(symbol, out op);
        }

        public bool TryGetUnaryOperator(string symbol, [NotNullWhen(true)] out IUnaryOperator? op)
        {
            return _unaryOperators.TryGetValue(symbol, out op);
        }

        public double GetValue(string name)
        {
            if(_variables.TryGetValue(name, out var result))
            {
                return result;
            }

            throw new Exception($"Cannot find the value named: {name}");
        }
    }
}
