using System;

namespace MathEngine.Functions.Common
{
    public sealed class MinFunction : IFunction
    {
        public string Name => "Min";

        public double Call(ReadOnlySpan<double> args)
        {
            if (args.Length > 1)
            {
                double min = args[0];
                for (int i = 1; i < args.Length; i++)
                {
                    min = Math.Min(min, args[i]);
                }

                return min;
            }

            throw new ArithmeticException($"Invalid number of arguments, expected 2 or more but {args.Length} was get.");
        }
    }
}
