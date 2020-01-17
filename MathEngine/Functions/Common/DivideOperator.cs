using System;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class DivideOperator : IBinaryOperator
    {
        public int Precedence => OperatorPrecedence.Normal;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "/";

        public double Evaluate(double left, double right)
        {
            if (right == 0)
            {
                throw new DivideByZeroException($"{left} / {right}");
            }

            return left / right;
        }
    }
}
