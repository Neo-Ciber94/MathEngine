using System;
using System.Collections.Generic;
using System.Text;

namespace MathEngine.Functions
{
    public class SubstractOperator : IBinaryOperator
    {
        public int Precedence => 0;

        public OperatorAssociativity Associativity => OperatorAssociativity.Left;

        public string Name => "-";

        public double Evaluate(double left, double right) => left - right;
    }
}
