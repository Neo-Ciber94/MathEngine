using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class CotangentFunction : IFunction
    {
        public string Name => "Cot";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return 1 / Math.Tan(args[0]);
        }
    }
}
