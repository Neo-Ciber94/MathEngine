using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicCosecantFunction : IFunction
    {
        public string Name => "Csch";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Sinh(args[0] * Consts.DegreeToRadians);
        }
    }
}
