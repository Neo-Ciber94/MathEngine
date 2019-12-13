namespace MathEngine
{
    public interface IMathEvaluator
    {
        public IMathContext Context { get; }
        public ITokenizer Tokenizer { get; }

        public double Evaluate(Token[] tokens);

        public double Evaluate(string expression)
        {
            var tokens = Tokenizer.GetTokens(expression);
            return Evaluate(tokens);
        }
    }
}