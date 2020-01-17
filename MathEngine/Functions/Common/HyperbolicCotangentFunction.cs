using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class HyperbolicCotangentFunction : IFunction
    {
        public string Name => "Coth";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Tanh(args[0] * Consts.DegreeToRadians);
        }
    }
}