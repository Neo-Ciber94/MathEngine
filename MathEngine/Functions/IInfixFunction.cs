namespace ExtraUtils.MathEngine.Functions
{
    /// <summary>
    /// Represents an infix function. When parsing this functions are handle different by the default <see cref="ExtraUtils.MathEngine.Tokenizer"/>,
    /// where symbols are handle as binary operators and names are handle as variables, constants and functions. 
    /// </summary>
    /// <seealso cref="ExtraUtils.MathEngine.Functions.IBinaryOperator" />
    public interface IInfixFunction : IBinaryOperator { }
}
