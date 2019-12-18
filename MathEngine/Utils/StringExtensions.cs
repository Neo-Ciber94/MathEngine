using System;
using System.Runtime.CompilerServices;

namespace MathEngine.Utils
{
    public static class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string RemoveWhiteSpaces(this string str)
        {
            return string.Join(string.Empty, str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char FirstChar(this string s)
        {
            return FirstCharOrNull(s)?? throw new ArgumentException("The string is empty");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char LastChar(this string s)
        {
            return LastCharOrNull(s) ?? throw new ArgumentException("The string is empty");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char? FirstCharOrNull(this string s)
        {
            return s.Length == 0? (char?)null : s[0];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char? LastCharOrNull(this string s)
        {
            return s.Length == 0 ? (char?)null : s[s.Length - 1];
        }
    }
}
