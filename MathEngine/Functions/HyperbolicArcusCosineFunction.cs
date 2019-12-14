using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicArcusCosineFunction : IFunction
    {
        public string Name => "ACosh";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Acosh(args[0]);
        }
    }
}
