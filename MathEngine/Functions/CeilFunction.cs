using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class CeilFunction : IFunction
    {
        public string Name => "Ceil";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Ceiling(args[0]);
        }
    }
}
