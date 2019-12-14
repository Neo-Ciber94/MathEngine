using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class MinFunction : IFunction
    {
        public string Name => "Min";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(2, args.Length);
            return Math.Min(args[0], args[1]);
        }
    }
}
