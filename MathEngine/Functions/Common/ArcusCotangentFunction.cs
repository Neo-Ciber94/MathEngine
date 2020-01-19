using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusCotangentFunction : IFunction
    {
        public string Name => "ACot";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return 1 / Math.Atan(args[0] * Consts.DegreeToRadians);
        }
    }
}
