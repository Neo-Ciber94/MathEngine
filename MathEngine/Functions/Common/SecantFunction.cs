using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class SecantFunction : IFunction
    {
        public string Name => "Sec";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Cos(args[0] * Consts.DegreeToRadians);
        }
    }
}
