namespace MathEngine
{
    public interface ITokenizer
    {
        public IMathContext Context { get; }

        public Token[] GetTokens(string expression);
    }
}