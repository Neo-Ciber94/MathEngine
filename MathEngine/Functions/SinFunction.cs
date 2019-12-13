using MathEngine.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathEngine.Functions
{
    public sealed class SinFunction : IFunction
    {
        public string Name => "Sin";

        public double Call(double[] args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Sin(args[0]);
        }
    }
}
