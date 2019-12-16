
namespace MathEngine.Functions.Common
{
    public sealed class PlusInfixFunction : IInfixFunction
    {
        public int Precedence => OperatorPrecedence.Low;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "Plus";

        public double Evaluate(double left, double right) => left + right;
    }
}
