
namespace MathEngine.Functions.Common
{
    public sealed class MinusOperator : IUnaryOperator
    {
        public OperatorNotation Notation => OperatorNotation.Prefix;

        public string Name => "-";

        public double Evaluate(double value)
        {
            return -value;
        }
    }
}
