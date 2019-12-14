using System;

namespace MathEngine.Functions
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
            switch (args.Length)
            {
                case 0:
                    return random.NextDouble();
                case 1:
                    return GetRandom(args[0]);
                case 2:
                    return GetRandom(args[0], args[1]);
                default:
                    throw new ArgumentException($"Invalid number of arguments, expected 0 to 2 arguments but {args.Length} was get.");
            }
        }

        private double GetRandom(double max)
        {
            return GetRandom(0, max);
        }

        private double GetRandom(double min, double max)
        {
            return random.NextDouble() * (max - min) + min;
        }
    }
}
