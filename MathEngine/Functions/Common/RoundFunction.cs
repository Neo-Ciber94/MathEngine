using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class RoundFunction : IFunction
    {
        public string Name => "Round";

        public int Arity => 1;
        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Round(args[0]);
        }
    }
}
