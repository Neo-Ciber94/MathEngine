using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusTangentFunction : IFunction
    {
        public string Name => "ATanh";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Atanh(args[0] * Consts.DegreeToRadians);
        }
    }
}