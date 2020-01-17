using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class CosineFunction : IFunction
    {
        public string Name => "Cos";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Cos(args[0] * Consts.DegreeToRadians);
        }
    }
}
