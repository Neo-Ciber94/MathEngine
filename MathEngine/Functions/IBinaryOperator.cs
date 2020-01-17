using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions
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

    /// <summary>
    /// Represents an operation performed over 2 values.
    /// </summary>
    /// <seealso cref="ExtraUtils.MathEngine.Functions.IFunction" />
    public interface IBinaryOperator : IFunction
    {
        /// <summary>
        /// Gets a value indicating the order which this operation will be performed.
        /// Is recomended to use <see cref="ExtraUtils.MathEngine.Functions.OperatorPrecedence"/> constants values for implement this property.
        /// <para></para>
        /// See also: <see href="https://en.wikipedia.org/wiki/Order_of_operations"/>
        /// </summary>
        public int Precedence { get; }
        /// <summary>
        /// Gets a value indicating how operators of the same precedence of this will be grouped in the absent of parentheses.
        /// <para></para>
        /// See also: <see href="https://en.wikipedia.org/wiki/Operator_associativity"/>
        /// </summary>
        public OperatorAssociativity Associativity { get; }

        /// <summary>
        /// Performs this operation over the given values.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>The result of this operation.</returns>
        public double Evaluate(double left, double right);

        double IFunction.Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(2, args.Length);
            return Evaluate(args[0], args[1]);
        }

        int IFunction.Arity => 2;
    }
}
