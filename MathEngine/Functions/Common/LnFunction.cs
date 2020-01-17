using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class LnFunction : IFunction
    {
        public string Name => "Ln";

        public int Arity => 1;

        public double Call(ReadOnlySpan<double> args)
        {
            Requires.ArgumentCount(1, args.Length);
            return Math.Log(args[0], Math.E);
        }
    }
}
