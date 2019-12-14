using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicCosineFunction : IFunction
    {
        public string Name => "Cosh";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Cosh(args[0]);
        }
    }
}