namespace MathEngine
{
    /// <summary>
    /// Provides a mechanism for extract the tokens from an expression.
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// The <see cref="IMathContext"/> used for the expression evaluation.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public IMathContext Context { get; }

        /// <summary>
        /// Gets the tokens from the specified expression.
        /// </summary>
        /// <param name="expression">The expression to use.</param>
        /// <returns>An array with all the tokens of the given expression.</returns>
        public Token[] GetTokens(string expression);
    }
}