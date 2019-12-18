using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class CosineFunction : IFunction
    {
        public string Name => "Cos";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Cos(args[0]);
        }
    }
}
