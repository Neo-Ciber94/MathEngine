using System;
using System.Collections.Generic;
using MathEngine.Utils;

namespace MathEngine
{
    public sealed class ExpressionEvaluationException : Exception
    {
        public ExpressionEvaluationException(string msg) : base(msg) { }

        public ExpressionEvaluationException(Stack<double> values, Token[] tokens) : base(GetMessage(values, tokens)) { }

        private static string GetMessage(Stack<double> values, Token[] tokens)
        {
            return $"Expression evaluation have failed. \nValues on stack: {values.AsString()}.\nExpression: {tokens.AsString()}";
        }
    }
}
