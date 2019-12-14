using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class MaxFunction : IFunction
    {
        public string Name => "Max";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(2, args.Length);
            return Math.Max(args[0], args[1]);
        }
    }
}
