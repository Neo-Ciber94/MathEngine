using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class HyperbolicArcusCosecantFunction : IFunction
    {
        public string Name => "ACsch";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return 1 / Math.Asinh(args[0]);
        }
    }
}
