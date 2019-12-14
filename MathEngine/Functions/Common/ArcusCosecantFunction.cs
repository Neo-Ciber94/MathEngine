using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class ArcusCosecantFunction : IFunction
    {
        public string Name => "ACsc";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return 1 / Math.Asin(args[0]);
        }
    }
}