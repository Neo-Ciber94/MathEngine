using System;
using System.Linq;

namespace MathEngine
{
    public class MathExpressionException : Exception
    {
        public MathExpressionException(Token[] tokens) : this(null, tokens) { }
        public MathExpressionException(string? msg, Token[] tokens) : base(GetMessage(msg, tokens)) { }

        public MathExpressionException(string expression) : this(null, expression) { }

        public MathExpressionException(string? msg, string expression) : base(GetMessage(msg, expression)) { }

        public static string GetMessage(string? msg, Token[] tokens)
        {
            var expression = tokens.Select(t => t.Value).Aggregate((cur, str) => cur + str);
            return GetMessage(msg, expression);
        }

        private static string GetMessage(string? msg, string expression)
        {
            if(msg == null)
            {
                msg = "Invalid expression";
            }

            return $"{msg}, {expression}";
        }
    }
}
