
namespace ExtraUtils.MathEngine.Functions.Common
{
    public sealed class MinusInfixFunction : IInfixFunction
    {
        public int Precedence => OperatorPrecedence.Low;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "Minus";

        public double Evaluate(double left, double right) => left - right;
    }
}
