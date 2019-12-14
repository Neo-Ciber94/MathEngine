using System;

namespace MathEngine.Functions.Common
{
    public sealed class MaxFunction : IFunction
    {
        public string Name => "Max";

        public double Call(ReadOnlySpan<double> args)
        {
            if(args.Length > 1)
            {
                double max = args[0];
                for(int i = 1; i < args.Length; i++)
                {
                    max = Math.Max(max, args[i]);
                }

                return max;
            }

            throw new ArithmeticException($"Invalid number of arguments, expected 2 or more but {args.Length} was get.");
        }
    }
}
