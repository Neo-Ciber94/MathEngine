using MathEngine.Utils;
using NUnit.Framework;
using System.Linq;

namespace MathEngine.Tests
{
    [TestFixture()]
    public class TokenizerTests
    {
        [Test()]
        public void GetTokensTest1()
        {
            var result = Tokenizer.Instance.GetTokens("10 + 5 * 2");
            var expected = new string[] { "10", "+", "5", "*", "2" };

            NUnit.Framework.Assert.AreEqual(5, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, Extensions.TokensToString(result), Extensions.CollectionToString(result));

            NUnit.Framework.Assert.AreEqual(new Token("10", TokenType.Number), result[0]);
            NUnit.Framework.Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[1]);
            NUnit.Framework.Assert.AreEqual(new Token("5", TokenType.Number), result[2]);
            NUnit.Framework.Assert.AreEqual(new Token("*", TokenType.BinaryOperator), result[3]);
            NUnit.Framework.Assert.AreEqual(new Token("2", TokenType.Number), result[4]);
        }

        [Test()]
        public void GetTokensTest2()
        {
            var result = Tokenizer.Instance.GetTokens("5+-2");
            var expected = new string[] { "5", "+", "-", "2"};

            NUnit.Framework.Assert.AreEqual(4, result.Length);
            CollectionAssert.AreEqual(expected, result.TokensToString(), result.CollectionToString(s => s.Value));

            NUnit.Framework.Assert.AreEqual(new Token("5", TokenType.Number), result[0]);
            NUnit.Framework.Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[1]);
            NUnit.Framework.Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[2]);
            NUnit.Framework.Assert.AreEqual(new Token("2", TokenType.Number), result[3]);
        }

        [Test()]
        public void GetTokensTest3()
        {
            var result = Tokenizer.Instance.GetTokens("max(5 + 2 * 1, 3 / -2)");
            var expected = new string[] { "max", "(", "5", "+", "2", "*", "1", ",", "3", "/", "-", "2", ")"};

            NUnit.Framework.Assert.AreEqual(13, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, Extensions.TokensToString(result), Extensions.CollectionToString(result));

            NUnit.Framework.Assert.AreEqual(new Token("max", TokenType.Function), result[0]);
            NUnit.Framework.Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[1]);
            NUnit.Framework.Assert.AreEqual(new Token("5", TokenType.Number), result[2]);
            NUnit.Framework.Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[3]);
            NUnit.Framework.Assert.AreEqual(new Token("2", TokenType.Number), result[4]);
            NUnit.Framework.Assert.AreEqual(new Token("*", TokenType.BinaryOperator), result[5]);
            NUnit.Framework.Assert.AreEqual(new Token("1", TokenType.Number), result[6]);
            NUnit.Framework.Assert.AreEqual(new Token(",", TokenType.Comma), result[7]);
            NUnit.Framework.Assert.AreEqual(new Token("3", TokenType.Number), result[8]);
            NUnit.Framework.Assert.AreEqual(new Token("/", TokenType.BinaryOperator), result[9]);
            NUnit.Framework.Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[10]);
            NUnit.Framework.Assert.AreEqual(new Token("2", TokenType.Number), result[11]);
            NUnit.Framework.Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[12]);
        }

        [Test()]
        public void GetTokensTest4()
        {
            var result = Tokenizer.Instance.GetTokens("-(10+(-2))");
            var expected = new string[] { "-", "(", "10", "+", "(", "-", "2", ")", ")"};

            NUnit.Framework.Assert.AreEqual(9, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, Extensions.TokensToString(result), Extensions.CollectionToString(result));

            NUnit.Framework.Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[0]);
            NUnit.Framework.Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[1]);
            NUnit.Framework.Assert.AreEqual(new Token("10", TokenType.Number), result[2]);
            NUnit.Framework.Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[3]);
            NUnit.Framework.Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[4]);
            NUnit.Framework.Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[5]);
            NUnit.Framework.Assert.AreEqual(new Token("2", TokenType.Number), result[6]);
            NUnit.Framework.Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[7]);
            NUnit.Framework.Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[8]);
        }

        [Test()]
        public void GetTokensTest5()
        {
            var expression = "(10 + (-5 * 2))";
            var result = Tokenizer.Instance.GetTokens(expression);
            var expected = new string[]
            {
                "(", "10", "+", "(", "-", "5", "*", "2", ")", ")"
            };

            NUnit.Framework.Assert.AreEqual(10, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, result.TokensToString(), result.CollectionToString(s => s.Value));

            NUnit.Framework.Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[0]);
            NUnit.Framework.Assert.AreEqual(new Token("10", TokenType.Number), result[1]);
            NUnit.Framework.Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[2]);
            NUnit.Framework.Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[3]);
            NUnit.Framework.Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[4]);
            NUnit.Framework.Assert.AreEqual(new Token("5", TokenType.Number), result[5]);
            NUnit.Framework.Assert.AreEqual(new Token("*", TokenType.BinaryOperator), result[6]);
            NUnit.Framework.Assert.AreEqual(new Token("2", TokenType.Number), result[7]);
            NUnit.Framework.Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[8]);
            NUnit.Framework.Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[9]);
        }
    }
}