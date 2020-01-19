using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class SumFunction : IFunction
    {
        public string Name => "Sum";

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.MinCount(1, args.Length);

            double total = 0;

            for(int i = 0; i < args.Length; i++)
            {
                total += args[i];
            }

            return total;
        }
    }
}
