
namespace MathEngine.Functions.Common
{
    public sealed class AddOperator : IBinaryOperator
    {
        public int Precedence => 0;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "+";

        public double Evaluate(double left, double right) => left + right;
    }
}