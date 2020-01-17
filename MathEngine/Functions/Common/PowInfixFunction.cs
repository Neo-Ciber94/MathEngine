using System;

namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class PowInfixFunction : IInfixFunction
    {
        public int Precedence => OperatorPrecedence.High;

        public OperatorAssociativity Associativity => OperatorAssociativity.Right;

        public string Name => "Pow";

        public double Evaluate(double left, double right) => Math.Pow(left, right);
    }
}
