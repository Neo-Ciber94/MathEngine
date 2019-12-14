using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class HyperbolicCotangentFunction : IFunction
    {
        public string Name => "Coth";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return 1 / Math.Tanh(args[0]);
        }
    }
}