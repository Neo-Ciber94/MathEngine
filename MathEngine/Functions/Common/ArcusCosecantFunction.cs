using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class ArcusCosecantFunction : IFunction
    {
        public string Name => "ACsc";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Asin(args[0] * Consts.DegreeToRadians);
        }
    }
}