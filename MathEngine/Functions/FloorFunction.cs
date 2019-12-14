using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class FloorFunction : IFunction
    {
        public string Name => "Floor";

        public double Call(ReadOnlySpan<double> args)
        {
            Require.ArgumentCount(1, args.Length);
            return Math.Floor(args[0]);
        }
    }
}
