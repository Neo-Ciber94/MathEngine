using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class CosecantFunction : IFunction
    {
        public string Name => "Csc";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return 1 / Math.Sin(args[0]);
        }
    }
}
