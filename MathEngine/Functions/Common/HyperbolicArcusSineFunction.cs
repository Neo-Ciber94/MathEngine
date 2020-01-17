using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusSineFunction : IFunction
    {
        public string Name => "ASinh";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Asinh(args[0] * Consts.DegreeToRadians);
        }
    }
}