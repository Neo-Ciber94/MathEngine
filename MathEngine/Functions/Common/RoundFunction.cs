using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class RoundFunction : IFunction
    {
        public string Name => "Round";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Round(args[0]);
        }
    }
}
