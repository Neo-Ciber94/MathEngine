using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicArcusCosecantFunction : IFunction
    {
        public string Name => "ACsch";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return 1 / Math.Asinh(args[0]);
        }
    }
}
