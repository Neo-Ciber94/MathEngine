using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class SqrtFunction : IFunction
    {
        public string Name => "Sqrt";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Sqrt(args[0]);
        }
    }
}
