using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class SineFunction : IFunction
    {
        public string Name => "Sin";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Sin(args[0] * Consts.DegreeToRadians);
        }
    }
}
