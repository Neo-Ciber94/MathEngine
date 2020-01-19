using System;

namespace MathEngine
{
    /// <summary>
    /// An exception that is throw when the given number of arguments is invalid.
    /// </summary>
    /// <seealso cref="Exception" />
    public sealed class InvalidArgumentCountException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidArgumentCountException"/> class.
        /// </summary>
        /// <param name="msg">The error message.</param>
        public InvalidArgumentCountException(string msg) : base(msg) { }
    }
}
