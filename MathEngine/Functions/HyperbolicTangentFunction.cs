﻿using System;
using MathEngine.Utils;

namespace MathEngine.Functions
{
    public sealed class HyperbolicTangentFunction : IFunction
    {
        public string Name => "Tanh";

        public double Call(ReadOnlySpan<double> args)
        {
            Check.ArgumentCount(1, args.Length);
            return Math.Tanh(args[0]);
        }
    }
}
