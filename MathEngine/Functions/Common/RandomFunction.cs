using System;
using ExtraUtils.MathEngine.Utilities;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class RandomFunction : IFunction
    {
        private readonly Random random;

        public RandomFunction()
        {
            random = new Random();
        }

        public RandomFunction(int seed)
        {
            random = new Random(seed);
        }

        public string Name => "Random";

        public double Call(ReadOnlySpan<double> args)
        {
            Arguments.Count(0, 2, args.Length);

            return args.Length switch
            {
                0 => random.NextDouble(),
                1 => NextRange(0, args[0]),
                2 => NextRange(args[0], args[1]),
                _ => throw new ArgumentException()
            };
        }

        private double NextRange(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}
