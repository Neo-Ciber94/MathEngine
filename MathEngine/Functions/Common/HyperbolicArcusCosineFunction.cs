using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusCosineFunction : IFunction
    {
        public string Name => "ACosh";

        public int Arity => 1;
        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Acosh(args[0] * Consts.DegreeToRadians);
        }
    }
}
