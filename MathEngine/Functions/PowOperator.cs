using System;

namespace MathEngine.Functions
{
    public sealed class PowOperator : IBinaryOperator
    {
        public int Precedence => 2;

        public OperatorAssociativity Associativity => OperatorAssociativity.Right;

        public string Name => "^";

        public double Evaluate(double left, double right)
        {
            return Math.Pow(left, right);
        }
    }
}
