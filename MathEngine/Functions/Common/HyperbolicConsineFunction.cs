using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class HyperbolicCosineFunction : IFunction
    {
        public string Name => "Cosh";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Cosh(args[0] * Consts.DegreeToRadians);
        }
    }
}