using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ExtraUtils.MathEngine.Utilities
{
    internal static class Requires
    {
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentCount(int expected, int actual)
        {
            if (expected != actual)
            {
                throw new ArgumentException($"Invalid number of arguments, expected {expected} but {actual} was get.");
            }
        }
    }
}
