using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicSecantFunction : IFunction
    {
        public string Name => "Sech";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return 1 / Math.Cosh(args[0]);
        }
    }
}
