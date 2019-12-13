using System;

namespace MathEngine.Functions
{
    public enum OperatorAssociativity { Right, Left }
    public interface IBinaryOperator : IFunction
    {
        public int Precedence { get; }
        public OperatorAssociativity Associativity { get; }

        public double Evaluate(double left, double right);

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
}
