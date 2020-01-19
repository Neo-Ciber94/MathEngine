using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusTangentFunction : IFunction
    {
        public string Name => "ATan";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Atan(args[0] * Consts.DegreeToRadians);
        }
    }
}
