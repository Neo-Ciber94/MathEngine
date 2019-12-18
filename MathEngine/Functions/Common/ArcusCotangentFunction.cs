using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class ArcusCotangentFunction : IFunction
    {
        public string Name => "ACot";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Atan(args[0] * Consts.DegreeToRadians);
        }
    }
}
