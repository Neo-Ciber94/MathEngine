namespace ExtraUtils.MathEngine.Functions.Common
{
    public class SubstractOperator : IBinaryOperator
    {
        public int Precedence => OperatorPrecedence.Low;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "-";

        public double Evaluate(double left, double right) => left - right;
    }
}
