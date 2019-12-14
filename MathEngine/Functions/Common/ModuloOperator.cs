
namespace MathEngine.Functions.Common
{
    public sealed class ModuloOperator : IInfixFunction
    {
        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "Mod";

        public int Precedence => 2;

        public double Evaluate(double left, double right)
        {
            return left % right;
        }
    }
}
