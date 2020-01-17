using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusSecantFunction : IFunction
    {
        public string Name => "ASec";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Acos(args[0] * Consts.DegreeToRadians);
        }
    }
}
