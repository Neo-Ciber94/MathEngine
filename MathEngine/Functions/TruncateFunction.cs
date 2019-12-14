using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class TruncateFunction : IFunction
    {
        public string Name => "Truncate";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Truncate(args[0]);
        }
    }
}
