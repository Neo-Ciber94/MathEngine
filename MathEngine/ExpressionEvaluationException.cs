using System;
using System.Collections.Generic;
using System.Linq;

namespace MathEngine
{
    /// <summary>
    /// Represents an error that occur when evaluating a expression.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public sealed class ExpressionEvaluationException : Exception
    {
        /// <summary>
        /// Gets the expression tokens.
        /// </summary>
        /// <value>
        /// The expression tokens.
        /// </value>
        public IReadOnlyCollection<Token> ExpressionTokens { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionEvaluationException"/> class.
        /// </summary>
        /// <param name="expressionTokens">The expression tokens.</param>
        public ExpressionEvaluationException(IReadOnlyCollection<Token> expressionTokens) : base(BuildMessage(expressionTokens))
        {
            ExpressionTokens = expressionTokens;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionEvaluationException"/> class.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="expressionTokens">The expression tokens.</param>
        public ExpressionEvaluationException(string msg, IReadOnlyCollection<Token> expressionTokens) : base(msg)
        {
            ExpressionTokens = expressionTokens;
        }

        static string BuildMessage(IReadOnlyCollection<Token> expressionTokens)
        {
            return expressionTokens
                .Where(t => t.Type != TokenType.ArgCount)
                .Select(t => t.Value)
                .Aggregate((cur, str) => $"{cur} {str}");
        }
    }
}
