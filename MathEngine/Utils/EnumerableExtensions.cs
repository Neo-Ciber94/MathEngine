using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEngine.Utils
{
    public static class EnumerableExtensions
    {
        private const string DefaultSeparator = ", ";

        public static string ToString<T>(this IEnumerable<T> enumerable, Func<T, string> valueToString) => ToString(enumerable, DefaultSeparator, valueToString);

        public static string ToString<T>(this IEnumerable<T> enumerable, string? separator) => ToString(enumerable, separator, (s) => s == null? string.Empty: s.ToString()!);

        public static string ToString<T>(this IEnumerable<T> enumerable, string? separator, Func<T, string> valueToString)
        {
            StringBuilder sb = new StringBuilder("[");
            sb.AppendJoin(separator, enumerable.Select(valueToString));
            sb.Append("]");
            return sb.ToString();
        }
    }
}
