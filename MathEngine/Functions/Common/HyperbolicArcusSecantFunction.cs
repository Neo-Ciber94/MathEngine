using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusSecantFunction : IFunction
    {
        public string Name => "ASech";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Acosh(args[0] * Consts.DegreeToRadians);
        }
    }
}