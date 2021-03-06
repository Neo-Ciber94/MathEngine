
namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class PlusOperator : IUnaryOperator
    {
        public OperatorNotation Notation => OperatorNotation.Prefix;

        public string Name => "+";

        public double Evaluate(double value)
        {
            return value;
        }
    }
}
