using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusTangentFunction : IFunction
    {
        public string Name => "ATanh";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Atanh(args[0] * Consts.DegreeToRadians);
        }
    }
}