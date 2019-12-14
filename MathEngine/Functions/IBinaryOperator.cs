using System;

namespace MathEngine.Functions
{
    /// <summary>
    /// Represents how operators of the same precedence will be grouped in the absent of parentheses.
    /// </summary>
    public enum OperatorAssociativity 
    {
        /// <summary>
        /// Associate operators from right to left. Used for exponentiation.
        /// </summary>
        Right,
        /// <summary>
        /// Associate operators from left to right. Used for addition, substraction, multiplication and division.
        /// </summary>
        Left
    }
    public interface IBinaryOperator : IFunction
    {
        public int Precedence { get; }
        public OperatorAssociativity Associativity { get; }

        public double Evaluate(double left, double right);

        double IFunction.Call(ReadOnlySpan<double> args)
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
