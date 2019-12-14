using System;

namespace MathEngine.Functions
{
    public enum OperatorNotation { Prefix, Postfix }

    public interface IUnaryOperator : IFunction
    {
        public double Evaluate(double value);

        double IFunction.Call(ReadOnlySpan<double> args)
        {
            if (args.Length != Arity)
            {
                throw new ArgumentException($"Invalid number of arguments. {Arity} expected.");
            }

            return Evaluate(args[0]);
        }

        int IFunction.Arity => 1;

        public OperatorNotation Notation { get; }
    }
}
