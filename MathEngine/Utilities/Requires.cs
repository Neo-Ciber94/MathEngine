using System;
using System.Diagnostics;

namespace ExtraUtils.MathEngine.Utilities
{
    public static class Requires
    {
        [Conditional("DEBUG")]
        public static void ArgumentCount(int expected, int actual)
        {
            if (expected != actual)
            {
                throw new ArgumentException($"Invalid number of arguments, expected {expected} but {actual} was get.");
            }
        }
    }
}
