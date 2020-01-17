using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class CosineFunction : IFunction
    {
        public string Name => "Cos";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Cos(args[0] * Consts.DegreeToRadians);
        }
    }
}
