using System;
using MathEngine.Utils;

namespace MathEngine.Functions.Common
{
    public sealed class FloorFunction : IFunction
    {
        public string Name => "Floor";

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Floor(args[0]);
        }
    }
}
