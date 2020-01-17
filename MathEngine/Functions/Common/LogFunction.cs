using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class LogFunction : IFunction
    {
        public string Name => "Log";

        public double Call(ReadOnlySpan<double> args)
        {
            if (args.Length == 1)
            {
                return Math.Log10(args[0]);
            }
            else if (args.Length == 2)
            {
                return Math.Log(args[0], args[1]);
            }
            else
            {
                Requires.ArgumentCount(2, args.Length);
                return 0;
            }
        }
    }
}
