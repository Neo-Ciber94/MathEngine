using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class CotangentFunction : IFunction
    {
        public string Name => "Cot";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Tan(args[0] * Consts.DegreeToRadians);
        }
    }
}
