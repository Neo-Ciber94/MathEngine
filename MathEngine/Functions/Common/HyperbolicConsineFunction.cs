using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicCosineFunction : IFunction
    {
        public string Name => "Cosh";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Cosh(args[0] * Consts.DegreeToRadians);
        }
    }
}