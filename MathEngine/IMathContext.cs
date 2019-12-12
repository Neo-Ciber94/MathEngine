using MathEngine.Functions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

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

        public IReadOnlyDictionary<char, IBinaryOperator> Operators { get; }
        public IReadOnlyDictionary<char, IUnaryOperator> UnaryOperators { get; }
        public IReadOnlyDictionary<string, IFunction> Functions { get; }
        public IReadOnlyDictionary<string, double> Variables { get; }

        public bool IsOperator(char value) => Operators.ContainsKey(value) || UnaryOperators.ContainsKey(value);

        public bool IsFunction(string name) => Functions.ContainsKey(name);

        public bool IsVariable(string name) => Variables.ContainsKey(name);

        public bool TryGetBinaryOperator(char symbol, out IBinaryOperator? op) => Operators.TryGetValue(symbol, out op);

        public bool TryGetUnaryOperator(char symbol, out IUnaryOperator? op) => UnaryOperators.TryGetValue(symbol, out op);

        public bool TryGetFunction(string functionName, out IFunction? func) => Functions.TryGetValue(functionName, out func);

        public double Evaluate(double a, double b, char operation)
        {
            if(Operators.TryGetValue(operation, out var op))
            {
                return op.Evaluate(a, b);
            }

            throw new InvalidOperationException($"Cannot find the given operator: {operation}.");
        }

        public double Evaluate(double a, char operation)
        {
            if (UnaryOperators.TryGetValue(operation, out var op))
            {
                return op.Evaluate(a);
            }

            throw new InvalidOperationException($"Cannot find the given operator: {operation}.");
        }

        public double CallFunction(string functionName, double[] args)
        {
            if (Functions.TryGetValue(functionName, out var func))
            {
                return func.Call(args);
            }

            throw new InvalidOperationException($"Cannot find the given function: {functionName}.");
        }

        public double GetValue(string variableName)
        {
            if (Variables.TryGetValue(variableName, out var value))
            {
                return value;
            }

            throw new InvalidOperationException($"Cannot find the given variable: {variableName}.");
        }
    }

    class DefaultMathContext : IMathContext
    {
        private IReadOnlyDictionary<char, IBinaryOperator>? _operators;
        private IReadOnlyDictionary<char, IUnaryOperator>? _unaryOperators;
        private IReadOnlyDictionary<string, IFunction>? _functions;
        private IReadOnlyDictionary<string, double>? _variables;

        public IReadOnlyDictionary<char, IBinaryOperator> Operators
        {
            get
            {
                if(_operators == null)
                {
                    var builder = ImmutableDictionary.CreateBuilder<char, IBinaryOperator>();
                    builder.Add('+', new BinaryOperator('+', precedence: 0, Association.Left, (a, b) => (a + b)));
                    builder.Add('-', new BinaryOperator('-', precedence: 0, Association.Left, (a, b) => (a - b)));
                    builder.Add('*', new BinaryOperator('*', precedence: 1, Association.Left, (a, b) => (a * b)));
                    builder.Add('/', new BinaryOperator('/', precedence: 1, Association.Left, (a, b) => (a / b)));
                    builder.Add('^', new BinaryOperator('^', precedence: 2, Association.Right, (a, b) => Math.Pow(a, b)));
                    _operators = builder.ToImmutable();
                }

                return _operators;
            }
        }

        public IReadOnlyDictionary<char, IUnaryOperator> UnaryOperators
        {
            get
            {
                if (_unaryOperators == null)
                {
                    var builder = ImmutableDictionary.CreateBuilder<char, IUnaryOperator>();
                    builder.Add('+', new UnaryOperator('+', (a) => a));
                    builder.Add('-', new UnaryOperator('-', (a) => -a));
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
                    _variables = builder.ToImmutable();
                }

                return _variables;
            }
        }
    }
}
