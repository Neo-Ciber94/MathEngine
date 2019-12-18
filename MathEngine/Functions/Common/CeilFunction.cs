using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class CeilFunction : IFunction
    {
        public string Name => "Ceil";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Ceiling(args[0]);
        }
    }
}
