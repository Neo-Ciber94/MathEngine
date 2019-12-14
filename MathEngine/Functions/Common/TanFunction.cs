using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class TanFunction : IFunction
    {
        public string Name => "Tan";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Tan(args[0]);
        }
    }
}
