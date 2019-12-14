using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class ArcusCosineFunction : IFunction
    {
        public string Name => "ACos";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Acos(args[0]);
        }
    }
}