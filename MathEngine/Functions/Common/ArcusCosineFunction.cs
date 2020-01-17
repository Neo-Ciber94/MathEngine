using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusCosineFunction : IFunction
    {
        public string Name => "ACos";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Acos(args[0] * Consts.DegreeToRadians);
        }
    }
}