using System.Collections.Generic;
using System.Text;

namespace MathEngine.Utils
{
    public static class EnumerableExtensions
    {
        private const string DefaultSeparator = ", ";

        public static string AsString<T>(this IEnumerable<T> enumerable) => AsString(enumerable, DefaultSeparator);

        public static string AsString<T>(this IEnumerable<T> enumerable, string? separator)
        {
            StringBuilder sb = new StringBuilder("[");
            sb.AppendJoin(separator, enumerable);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
