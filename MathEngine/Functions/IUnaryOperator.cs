using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions
{
    /// <summary>
    /// Represents where an unary operator is positioned.
    /// </summary>
    public enum OperatorNotation 
    {
        /// <summary>
        /// The operator is positioned before.
        /// </summary>
        Prefix,
        /// <summary>
        /// The operator is positioned after.
        /// </summary>
        Postfix
    }

    /// <summary>
    /// Represents an operation performed over 1 value.
    /// </summary>
    /// <seealso cref="ExtraUtils.MathEngine.Functions.IFunction" />
    public interface IUnaryOperator : IFunction
    {
        /// <summary>
        /// Gets the position this operator.
        /// </summary>
        public OperatorNotation Notation { get; }

        /// <summary>
        /// Perform this operation over the given value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operation</returns>
        public double Evaluate(double value);

        double IFunction.Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Evaluate(args[0]);
        }

        int IFunction.Arity => 1;
    }
}
