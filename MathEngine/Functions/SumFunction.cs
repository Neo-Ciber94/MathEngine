using System;

namespace MathEngine.Functions
{
    public sealed class SumFunction : IFunction
    {
        public string Name => "Sum";
        public double Call(ReadOnlySpan<double> args)
        {
            double total = 0;
            foreach(var d in args)
            {
                total += d;
            }
            return total;
        }
    }
}
