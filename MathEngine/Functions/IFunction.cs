using System;

namespace ExtraUtils.MathEngine.Functions
{
    /// <summary>
    /// Represents a function.
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// Gets the name of this function.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the number of arguments this function takes or -1 if takes a variable number of arguments.
        /// </summary>
        public int Arity => -1;

        /// <summary>
        /// Execute this function over the given values.
        /// </summary>
        /// <param name="args">The arguments this function takes.</param>
        /// <returns>The result of the operation.</returns>
        public double Call(ReadOnlySpan<double> args);
    }
}
