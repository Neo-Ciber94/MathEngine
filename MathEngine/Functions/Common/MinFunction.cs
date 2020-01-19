using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class MinFunction : IFunction
    {
        public string Name => "Min";

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.MinCount(2, args.Length);

            double min = args[0];
            for (int i = 1; i < args.Length; i++)
            {
                min = Math.Min(min, args[i]);
            }

            return min;
        }
    }
}
