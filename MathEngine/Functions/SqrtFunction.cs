using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class SqrtFunction : IFunction
    {
        public string Name => "Sqrt";

        public double Call(double[] args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Sqrt(args[0]);
        }
    }
}
