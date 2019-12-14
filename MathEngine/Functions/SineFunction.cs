﻿using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class SineFunction : IFunction
    {
        public string Name => "Sin";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Sin(args[0]);
        }
    }
}
