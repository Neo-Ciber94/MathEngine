using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class ArcusSecantFunction : IFunction
    {
        public string Name => "ASec";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return 1 / Math.Acos(args[0]);
        }
    }
}
