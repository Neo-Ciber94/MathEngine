using MathEngine.Utils;
using System;

namespace MathEngine.Functions
{
    public sealed class CosFunction : IFunction
    {
        public string Name => "Cos";

        public double Call(double[] args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Cos(args[0]);
        }
    }
}
