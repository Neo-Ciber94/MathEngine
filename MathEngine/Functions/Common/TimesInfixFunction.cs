
namespace MathEngine.Functions.Common
{
    public sealed class TimesInfixFunction : IInfixFunction
    {
        public int Precedence => OperatorPrecedence.Normal;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "Times";

        public double Evaluate(double left, double right) => left * right;
    }
}
