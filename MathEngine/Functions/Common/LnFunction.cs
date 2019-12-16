using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class LnFunction : IFunction
    {
        public string Name => "Ln";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Log(args[0], Math.E);
        }
    }
}
