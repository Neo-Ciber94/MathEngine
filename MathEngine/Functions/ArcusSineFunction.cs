using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class ArcusSineFunction : IFunction
    {
        public string Name => "ASin";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Asin(args[0]);
        }
    }
}
