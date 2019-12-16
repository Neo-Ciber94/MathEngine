using System;
using System.Collections.Generic;
using System.Linq;
using MathEngine.Utils;

namespace MathEngine
{
    public sealed class ExpressionEvaluationException : Exception
    {
        public ExpressionEvaluationException(string msg) : base(msg) { }

        public ExpressionEvaluationException(string msg, Token[] tokens) : base(GetMessageWithTokens(msg, tokens)) { }

        internal ExpressionEvaluationException(Stack<double> values, Token[] tokens) : base(GetMessage(values, tokens)) { }

        public static string GetMessageWithTokens(string msg, Token[] tokens)
        {
            string expression = tokens.Select(t => t.Value).AsString();
            return $"{msg}\n\nExpression: {expression}" +
                $"\nExpression tokens: {tokens.AsString()}";
        }
        private static string GetMessage(Stack<double> values, Token[] tokens)
        {
            string expression = tokens.Select(t => t.Value).AsString();
            return $"Expression evaluation have failed." +
                $"\n\nValues on ouput stack: {values.AsString()}." +
                $"\nExpression: {expression}\nExpression tokens: {tokens.AsString()}";
        }
    }
}
