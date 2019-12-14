using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class HyperbolicSineFunction : IFunction
    {
        public string Name => "Sinh";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Sinh(args[0]);
        }
    }
}