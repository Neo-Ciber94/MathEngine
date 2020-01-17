using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusSineFunction : IFunction
    {
        public string Name => "ASinh";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Asinh(args[0] * Consts.DegreeToRadians);
        }
    }
}