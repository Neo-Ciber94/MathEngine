using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class CosecantFunction : IFunction
    {
        public string Name => "Csc";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Sin(args[0] * Consts.DegreeToRadians);
        }
    }
}
