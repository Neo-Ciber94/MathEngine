using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicCotangentFunction : IFunction
    {
        public string Name => "Coth";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return 1 / Math.Tanh(args[0]);
        }
    }
}