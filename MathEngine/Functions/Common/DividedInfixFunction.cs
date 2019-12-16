using System;

namespace MathEngine.Functions.Common
{
    public sealed class DividedInfixFunction : IInfixFunction
    {
        public int Precedence => OperatorPrecedence.Normal;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "Divided";

        public double Evaluate(double left, double right)
        {
            if (right == 0)
            {
                throw new DivideByZeroException($"{left}/{right}");
            }

            return left / right;
        }
    }
}
