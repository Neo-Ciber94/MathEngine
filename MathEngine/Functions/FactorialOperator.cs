using System;

namespace MathEngine.Functions
{
    public sealed class FactorialOperator : IUnaryOperator
    {
        public OperatorNotation Notation => OperatorNotation.Postfix;

        public string Name => "!";

        public double Evaluate(double value)
        {
            if(value < 0)
            {
                throw new ArithmeticException("Cannot apply factorial over a negative number.");
            }

            double result = 1;
            while(value > 0)
            {
                result *= (value--);
            }

            return result;
        }
    }
}
