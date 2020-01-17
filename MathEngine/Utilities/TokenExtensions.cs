using System.Collections.Generic;
using System.Text;

namespace ExtraUtils.MathEngine.Utilities
{
    public static class TokenExtensions
    {
        public static string[] ToStringArray(this IEnumerable<Token> tokens)
        {
            List<string> list = new List<string>();
            foreach (var t in tokens)
            {
                if (t.Type != TokenType.ArgCount)
                {
                    list.Add(t.Value);
                }
            }
            return list.ToArray();
        }

        public static string ToStringExpression(this IEnumerable<Token> tokens)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var t in tokens)
            {
                if (t.Type != TokenType.ArgCount)
                {
                    sb.Append(t.Value);
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }
    }
}
