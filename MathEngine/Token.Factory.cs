using System;
using System.Collections.Generic;

namespace MathEngine
{
    public partial class Token
    {
        public static readonly Token OpenParenthesis = new Token('(', TokenType.Parenthesis);
        public static readonly Token CloseParenthesis = new Token(')', TokenType.Parenthesis);
        public static readonly Token Comma = new Token(',', TokenType.Comma);

        public static Token FromNumber(double d)
        {
            return new Token(d.ToString(), TokenType.Number);
        }

        public static Token ArgCount(int n) => new Token(n.ToString(), TokenType.ArgCount);

        public static Token FromParenthesis(char c)
        {
            if (c == '(')
            {
                return OpenParenthesis;
            }
            else if (c == ')')
            {
                return CloseParenthesis;
            }
            else
            {
                throw new ArgumentException($"Given value is not a parenthesis: {c}");
            }
        }
    }

    public static class TokenExtensions
    {
        public static string[] ToStringExpression(this IEnumerable<Token> tokens)
        {
            List<string> list = new List<string>();
            foreach(var t in tokens)
            {
                if(t.Type != TokenType.ArgCount)
                {
                    list.Add(t.Value);
                }
            }
            return list.ToArray();
        }
    }
}
