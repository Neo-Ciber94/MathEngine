using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusCosecantFunction : IFunction
    {
        public string Name => "ACsch";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Asinh(args[0] * Consts.DegreeToRadians);
        }
    }
}
