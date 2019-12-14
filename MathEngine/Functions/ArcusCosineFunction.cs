using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class ArcusCosineFunction : IFunction
    {
        public string Name => "ACos";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Acos(args[0]);
        }
    }
}