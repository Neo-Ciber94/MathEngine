using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExtraUtils.MathEngine.Utilities
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AsString<T>(this IEnumerable<T> enumerable) => AsString(enumerable, ',');

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AsString<T>(this IEnumerable<T> enumerable, char separator)
        {
            return new StringBuilder('[')
                .AppendJoin(separator, enumerable)
                .Append(']')
                .ToString()
;        }
    }
}
