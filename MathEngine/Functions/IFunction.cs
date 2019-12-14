using System;

namespace MathEngine.Functions
{
    public interface IFunction
    {
        public string Name { get; }

        public int Arity => -1;

        public double Call(ReadOnlySpan<double> args);
    }
}
