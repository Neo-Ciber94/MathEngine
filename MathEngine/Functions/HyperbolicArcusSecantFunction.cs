using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicArcusSecantFunction : IFunction
    {
        public string Name => "ASech";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return 1 / Math.Acosh(args[0]);
        }
    }
}