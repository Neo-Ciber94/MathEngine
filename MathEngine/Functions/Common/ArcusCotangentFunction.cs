using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusCotangentFunction : IFunction
    {
        public string Name => "ACot";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Atan(args[0] * Consts.DegreeToRadians);
        }
    }
}
