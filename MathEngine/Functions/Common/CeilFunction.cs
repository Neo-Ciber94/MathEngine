using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class CeilFunction : IFunction
    {
        public string Name => "Ceil";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, args.Length);
            return Math.Ceiling(args[0]);
        }
    }
}
