using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ExtraUtils.MathEngine
{
    public class StringIgnoreCaseEqualityComparer : IEqualityComparer<string>
    {
        public static readonly StringIgnoreCaseEqualityComparer Instance = new StringIgnoreCaseEqualityComparer();

        private StringIgnoreCaseEqualityComparer() { }

        public bool Equals([AllowNull] string x, [AllowNull] string y) => string.Compare(x, y, ignoreCase: true) == 0;

        public int GetHashCode([DisallowNull] string obj) => string.GetHashCode(obj, StringComparison.OrdinalIgnoreCase);
    }
}
