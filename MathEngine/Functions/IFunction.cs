using System;

namespace MathEngine.Functions
{
    public enum Association { Right, Left }

    public interface IFunction
    {
        public string Name { get; }

        public int Arity => -1;

        public double Call(double[] args);
    }

    public interface IBinaryOperator : IFunction
    {
        public int Precedence { get; }
        public Association Associativity { get; }

        public double Evaluate(double a, double b);

        double IFunction.Call(double[] args)
        {
            if (args.Length != Arity)
            {
                throw new ArgumentException($"Invalid number of arguments. {Arity} expected.");
            }

            return Evaluate(args[0], args[1]);
        }

        int IFunction.Arity => 2;
    }

    public interface IUnaryOperator : IFunction
    {
        public double Evaluate(double d);

        double IFunction.Call(double[] args)
        {
            if (args.Length != Arity)
            {
                throw new ArgumentException($"Invalid number of arguments. {Arity} expected.");
            }

            return Evaluate(args[0]);
        }

        int IFunction.Arity => 1;
    }

    public interface IInfixFunction : IBinaryOperator { }
}
