using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicTangentFunction : IFunction
    {
        public string Name => "Tanh";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Tanh(args[0] * Consts.DegreeToRadians);
        }
    }
}
