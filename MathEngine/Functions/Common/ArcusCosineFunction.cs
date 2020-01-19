using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusCosineFunction : IFunction
    {
        public string Name => "ACos";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Acos(args[0] * Consts.DegreeToRadians);
        }
    }
}