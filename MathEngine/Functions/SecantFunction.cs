﻿using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class SecantFunction : IFunction
    {
        public string Name => "Sec";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return 1 / Math.Cos(args[0]);
        }
    }
}