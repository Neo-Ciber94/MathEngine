using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class ArcusCosecantFunction : IFunction
    {
        public string Name => "ACsc";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return 1 / Math.Asin(args[0]);
        }
    }
}