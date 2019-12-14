namespace MathEngine.Functions
{
    public sealed class MultiplyOperator : IBinaryOperator
    {
        public int Precedence => 1;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "*";

        public double Evaluate(double left, double right) => left * right;
    }
}
