
namespace ExtraUtils.MathEngine.Functions
{
    /// <summary>
    /// Contains constants values for represent the precedences of binary operators.
    /// </summary>
   public static class OperatorPrecedence
    {
        /// <summary>
        /// A lower precedence than addition and substraction.
        /// </summary>
        public const int VeryLow = 0;
        /// <summary>
        /// Low precedence, used for addition and substraction.
        /// </summary>
        public const int Low = 1;
        /// <summary>
        /// Normal precedence used for multiplication and division.
        /// </summary>
        public const int Normal = 2;
        /// <summary>
        /// High precedence used for exponentiation.
        /// </summary>
        public const int High = 3;
        /// <summary>
        /// A higher precedence than exponentiation.
        /// </summary>
        public const int VeryHigh = 4;
    }
}
