using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicArcusSineFunction : IFunction
    {
        public string Name => "ASinh";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Asinh(args[0]);
        }
    }
}