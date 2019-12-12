using System;

namespace MathEngine.Functions
{
    public class Function : IFunction
    {
        private readonly Func<double[], double> _func;

        public Function(string functionName, int arity, Func<double[], double> function)
        {
            Name = functionName ?? throw new ArgumentNullException(nameof(functionName));
            _func = function ?? throw new ArgumentNullException(nameof(function));
            Arity = arity;
        }

        public string Name { get;  }

        public int Arity { get; }

        public double Call(double[] args)
        {
            if(args.Length != Arity)
            {
                throw new ArgumentException($"Invalid number of arguments. {Arity} expected.");
            }

            return _func(args);
        }
    }

    public class BinaryOperator : IBinaryOperator
    {
        private readonly Func<double, double, double> _func;

        public BinaryOperator(char operatorSymbol, int precedence, Association associativity, Func<double, double, double> operation)
        {
            Name = operatorSymbol.ToString();
            Precedence = precedence;
            Associativity = associativity;
            _func = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        public string Name { get; }

        public int Precedence { get; }

        public Association Associativity { get; }

        public double Evaluate(double a, double b) => _func(a, b);
    }

    public class UnaryOperator : IUnaryOperator
    {
        public readonly Func<double, double> _func;

        public UnaryOperator(char operatorSymbol, Func<double, double> operation)
        {
            Name = operatorSymbol.ToString();
            _func = operation;
        }

        public string Name { get; }

        public double Evaluate(double d) => _func(d);
    }
}
