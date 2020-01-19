using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class MaxFunction : IFunction
    {
        public string Name => "Max";

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.MinCount(2, args.Length);

            double max = args[0];
            for (int i = 1; i < args.Length; i++)
            {
                max = Math.Max(max, args[i]);
            }

            return max;
        }
    }
}
