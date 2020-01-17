using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusTangentFunction : IFunction
    {
        public string Name => "ATan";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Atan(args[0] * Consts.DegreeToRadians);
        }
    }
}
