using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicArcusCotangentFunction : IFunction
    {
        public string Name => "ACoth";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return 1 / Math.Atanh(args[0]);
        }
    }
}
