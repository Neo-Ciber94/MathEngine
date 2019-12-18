using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class TangentFunction : IFunction
    {
        public string Name => "Tan";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Tan(args[0] * Consts.DegreeToRadians);
        }
    }
}
