using System;

namespace MathEngine
{
    public interface IFunction
    {
        public string Name { get; }

        public int Arity => -1;

        public double Call(double[] args);
    }

    public interface IInfixFunction : IFunction
    {
        public double Call(double a, double b);

        double IFunction.Call(double[] args)
        {
            if (args.Length != Arity)
            {
                throw new ArgumentException($"Invalid number of arguments. {Arity} expected.");
            }

            return Call(args[0], args[1]);
        }

        int IFunction.Arity => 2;
    }
}
