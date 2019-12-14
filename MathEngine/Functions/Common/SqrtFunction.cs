using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class SqrtFunction : IFunction
    {
        public string Name => "Sqrt";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Sqrt(args[0]);
        }
    }
}