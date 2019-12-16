
namespace MathEngine.Functions.Common
{
    public sealed class ModuloInfixFunction : IInfixFunction
    {
        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "Mod";

        public int Precedence => OperatorPrecedence.Normal;

        public double Evaluate(double left, double right)
        {
            return left % right;
        }
    }
}
