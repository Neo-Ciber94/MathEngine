using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicSineFunction : IFunction
    {
        public string Name => "Sinh";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Sinh(args[0] * Consts.DegreeToRadians);
        }
    }
}