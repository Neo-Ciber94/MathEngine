using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class HyperbolicTangentFunction : IFunction
    {
        public string Name => "Tanh";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Tanh(args[0] * Consts.DegreeToRadians);
        }
    }
}
