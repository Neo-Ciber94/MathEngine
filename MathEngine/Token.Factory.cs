using System;

namespace ExtraUtils.MathEngine
{
    public partial class Token
    {
        /// <summary>
        /// An open parenthessis token.
        /// </summary>
        public static readonly Token OpenParenthesis = new Token('(', TokenType.Parenthesis);
        /// <summary>
        /// A close parenthessis token.
        /// </summary>
        public static readonly Token CloseParenthesis = new Token(')', TokenType.Parenthesis);
        /// <summary>
        /// A comma token.
        /// </summary>
        public static readonly Token Comma = new Token(',', TokenType.Comma);

        /// <summary>
        /// Creates a token from the given number.
        /// </summary>
        /// <param name="d">The number.</param>
        /// <returns>A new token for the given value.</returns>
        public static Token FromNumber(double d)
        {
            return new Token(d.ToString(), TokenType.Number);
        }

        /// <summary>
        /// Creates a token that represents the number of arguments a function takes.
        /// </summary>
        /// <param name="n">The number of arguments the function take.</param>
        /// <returns>A new token for the specified value.</returns>
        public static Token ArgCount(int n) => new Token(n.ToString(), TokenType.ArgCount);

        /// <summary>
        /// Gets a token representing a parenthesis.
        /// </summary>
        /// <param name="c">The parentheses.</param>
        /// <returns>A token for a parenthesis.</returns>
        /// <exception cref="ArgumentException">If the given value if not a parenthesis.</exception>
        public static Token FromParenthesis(char c)
        {
            return c switch
            {
                '(' => OpenParenthesis,
                ')' => CloseParenthesis,
                _ => throw new ArgumentException($"Given value is not a parenthesis: {c}")
            };
        }
    }
}
