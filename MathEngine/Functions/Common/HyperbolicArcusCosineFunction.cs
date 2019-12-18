using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusCosineFunction : IFunction
    {
        public string Name => "ACosh";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Acosh(args[0] * Consts.DegreeToRadians);
        }
    }
}
