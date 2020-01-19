using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class CosecantFunction : IFunction
    {
        public string Name => "Csc";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return 1 / Math.Sin(args[0] * Consts.DegreeToRadians);
        }
    }
}
