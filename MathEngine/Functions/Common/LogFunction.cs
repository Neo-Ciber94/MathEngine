using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class LogFunction : IFunction
    {
        public string Name => "Log";

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(1, 2, args.Length);

            return args.Length switch
            {
                1 => Math.Log10(args[0]),
                2 => Math.Log(args[0], args[1]),
                _ => throw new ArgumentException()
            };
        }
    }
}
