using System;
using ExtraUtils.MathEngine.Utils;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class ArcusSineFunction : IFunction
    {
        public string Name => "ASin";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Asin(args[0] * Consts.DegreeToRadians);
        }
    }
}
