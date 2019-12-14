using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicArcusTangentFunction : IFunction
    {
        public string Name => "ATanh";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Atanh(args[0]);
        }
    }
}