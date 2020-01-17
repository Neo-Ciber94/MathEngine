using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusCotangentFunction : IFunction
    {
        public string Name => "ACoth";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Atanh(args[0] * Consts.DegreeToRadians);
        }
    }
}
