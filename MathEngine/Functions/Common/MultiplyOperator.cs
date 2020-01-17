namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class MultiplyOperator : IBinaryOperator
    {
        public int Precedence => OperatorPrecedence.Normal;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "*";

        public double Evaluate(double left, double right) => left * right;
    }
}
