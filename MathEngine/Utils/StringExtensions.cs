using System;
using System.IO;

namespace MathEngine.Utils
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpaces(this string str)
        {
            return string.Join(string.Empty, str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
