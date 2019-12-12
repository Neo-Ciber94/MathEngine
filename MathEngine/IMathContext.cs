using MathEngine.Functions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MathEngine
{
    public interface IMathContext
    {
        private static IMathContext _default;
        public static IMathContext Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new DefaultMathContext();
                }

                return _default;
            }
        }

        public IReadOnlyDictionary<string, IBinaryOperator> Operators { get; }
        public IReadOnlyDictionary<string, IUnaryOperator> UnaryOperators { get; }
        public IReadOnlyDictionary<string, IFunction> Functions { get; }
        public IReadOnlyDictionary<string, double> Variables { get; }

        protected string GetString(string s) => s;

        public bool IsOperator(string value) => Operators.ContainsKey(value) || UnaryOperators.ContainsKey(value);

        public bool IsOperator(char value) => IsOperator(value.ToString());

        public bool IsFunction(string name) => Functions.ContainsKey(GetString(name));

        public bool IsVariable(string name) => Variables.ContainsKey(GetString(name));

        public bool TryGetBinaryOperator(string symbol, out IBinaryOperator? op) => Operators.TryGetValue(symbol, out op);

        public bool TryGetUnaryOperator(string symbol, out IUnaryOperator? op) => UnaryOperators.TryGetValue(symbol, out op);

        public bool TryGetFunction(string functionName, out IFunction? func) => Functions.TryGetValue(GetString(functionName), out func);

        public double Evaluate(double a, double b, string operation)
        {
            if (Operators.TryGetValue(operation, out var op))
            {
                return op.Evaluate(a, b);
            }

            throw new InvalidOperationException($"Cannot find the given operator: {operation}.");
        }

        public double Evaluate(double a, string operation)
        {
            if (UnaryOperators.TryGetValue(operation, out var op))
            {
                return op.Evaluate(a);
            }

            throw new InvalidOperationException($"Cannot find the given operator: {operation}.");
        }

        public double CallFunction(string functionName, double[] args)
        {
            if (Functions.TryGetValue(GetString(functionName), out var func))
            {
                return func.Call(args);
            }

            throw new InvalidOperationException($"Cannot find the given function: {functionName}.");
        }

        public double GetValue(string variableName)
        {
            if (Variables.TryGetValue(GetString(variableName), out var value))
            {
                return value;
            }

            throw new InvalidOperationException($"Cannot find the given variable: {variableName}.");
        }
    }

    class DefaultMathContext : IMathContext
    {
        private IReadOnlyDictionary<string, IBinaryOperator>? _operators;
        private IReadOnlyDictionary<string, IUnaryOperator>? _unaryOperators;
        private IReadOnlyDictionary<string, IFunction>? _functions;
        private IReadOnlyDictionary<string, double>? _variables;

        protected string GetString(string s) => s.ToLower();

        public IReadOnlyDictionary<string, IBinaryOperator> Operators
        {
            get
            {
                if (_operators == null)
                {
                    var builder = ImmutableDictionary.CreateBuilder<string, IBinaryOperator>();
                    builder.Add("+", new BinaryOperator('+', precedence: 0, Association.Left, (a, b) => (a + b)));
                    builder.Add("-", new BinaryOperator('-', precedence: 0, Association.Left, (a, b) => (a - b)));
                    builder.Add("*", new BinaryOperator('*', precedence: 1, Association.Left, (a, b) => (a * b)));
                    builder.Add("/", new BinaryOperator('/', precedence: 1, Association.Left, (a, b) => (a / b)));
                    builder.Add("^", new BinaryOperator('^', precedence: 2, Association.Right, (a, b) => Math.Pow(a, b)));
                    _operators = builder.ToImmutable();
                }

                return _operators;
            }
        }

        public IReadOnlyDictionary<string, IUnaryOperator> UnaryOperators
        {
            get
            {
                if (_unaryOperators == null)
                {
                    var builder = ImmutableDictionary.CreateBuilder<string, IUnaryOperator>();
                    builder.Add("+", new UnaryOperator('+', (a) => a));
                    builder.Add("-", new UnaryOperator('-', (a) => -a));
                    _unaryOperators = builder.ToImmutable();
                }

                return _unaryOperators;
            }
        }

        public IReadOnlyDictionary<string, IFunction> Functions
        {
            get
            {
                if (_functions == null)
                {
                    var builder = ImmutableDictionary.CreateBuilder<string, IFunction>();
                    builder.Add("max", new Function("max", arity: 2, (args) => Math.Max(args[0], args[1])));
                    builder.Add("min", new Function("min", arity: 2, (args) => Math.Min(args[0], args[1])));
                    builder.Add("mod", new Function("mod", arity: 2, (args) => args[0] % args[1]));
                    _functions = builder.ToImmutable();
                }

                return _functions;
            }
        }

        public IReadOnlyDictionary<string, double> Variables
        {
            get
            {
                if (_variables == null)
                {
                    var builder = ImmutableDictionary.CreateBuilder<string, double>();
                    builder.Add("pi", Math.PI);
                    builder.Add("e", Math.E);
                    _variables = builder.ToImmutable();
                }

                return _variables;
            }
        }
    }
}
