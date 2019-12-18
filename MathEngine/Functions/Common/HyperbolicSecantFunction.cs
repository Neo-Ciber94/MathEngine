using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class HyperbolicSecantFunction : IFunction
    {
        public string Name => "Sech";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Cosh(args[0]);
        }
    }
}
