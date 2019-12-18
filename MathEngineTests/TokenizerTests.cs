using MathEngine.Utils;
using NUnit.Framework;

namespace MathEngine.Tests
{
    [TestFixture()]
    public class TokenizerTests
    {
        [Test()]
        public void GetTokensTest1([Values("10 + 5 * 2", "10+5*2")] string expression)
        {
            var result = Tokenizer.Default.GetTokens(expression);
            var expected = new string[] { "10", "+", "5", "*", "2" };

            Assert.AreEqual(5, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, Extensions.ToStringExpression(result), Extensions.CollectionToString(result));

            Assert.AreEqual(new Token("10", TokenType.Number), result[0]);
            Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[1]);
            Assert.AreEqual(new Token("5", TokenType.Number), result[2]);
            Assert.AreEqual(new Token("*", TokenType.BinaryOperator), result[3]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[4]);
        }

        [Test()]
        public void GetTokensTest2([Values("5 + - 2", "5+-2")] string expression)
        {
            var result = Tokenizer.Default.GetTokens("5 + - 2");
            var expected = new string[] { "5", "+", "-", "2" };

            Assert.AreEqual(4, result.Length);
            CollectionAssert.AreEqual(expected, result.ToStringExpression(), result.CollectionToString(s => s.Value));

            Assert.AreEqual(new Token("5", TokenType.Number), result[0]);
            Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[1]);
            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[2]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[3]);
        }

        [Test()]
        public void GetTokensTest3()
        {
            var result = Tokenizer.Default.GetTokens("max(5 + 2 * 1, 3 / -2)");
            var expected = new string[] { "max", "(", "5", "+", "2", "*", "1", ",", "3", "/", "-", "2", ")" };

            Assert.AreEqual(13, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, Extensions.ToStringExpression(result), Extensions.CollectionToString(result));

            Assert.AreEqual(new Token("max", TokenType.Function), result[0]);
            Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[1]);
            Assert.AreEqual(new Token("5", TokenType.Number), result[2]);
            Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[3]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[4]);
            Assert.AreEqual(new Token("*", TokenType.BinaryOperator), result[5]);
            Assert.AreEqual(new Token("1", TokenType.Number), result[6]);
            Assert.AreEqual(new Token(",", TokenType.Comma), result[7]);
            Assert.AreEqual(new Token("3", TokenType.Number), result[8]);
            Assert.AreEqual(new Token("/", TokenType.BinaryOperator), result[9]);
            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[10]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[11]);
            Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[12]);
        }

        [Test()]
        public void GetTokensTest4()
        {
            var result = Tokenizer.Default.GetTokens("-(10+(-2))");
            var expected = new string[] { "-", "(", "10", "+", "(", "-", "2", ")", ")" };

            Assert.AreEqual(9, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, Extensions.ToStringExpression(result), Extensions.CollectionToString(result));

            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[0]);
            Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[1]);
            Assert.AreEqual(new Token("10", TokenType.Number), result[2]);
            Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[3]);
            Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[4]);
            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[5]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[6]);
            Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[7]);
            Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[8]);
        }

        [Test()]
        public void GetTokensTest5()
        {
            var expression = "(10 + (-5 * 2))";
            var result = Tokenizer.Default.GetTokens(expression);
            var expected = new string[]
            {
                "(", "10", "+", "(", "-", "5", "*", "2", ")", ")"
            };

            Assert.AreEqual(10, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, result.ToStringExpression(), result.CollectionToString(s => s.Value));

            Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[0]);
            Assert.AreEqual(new Token("10", TokenType.Number), result[1]);
            Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[2]);
            Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[3]);
            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[4]);
            Assert.AreEqual(new Token("5", TokenType.Number), result[5]);
            Assert.AreEqual(new Token("*", TokenType.BinaryOperator), result[6]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[7]);
            Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[8]);
            Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[9]);
        }

        [Test()]
        public void GetTokensTest6()
        {
            var expression = "(5 * 2) + 1";
            var result = Tokenizer.Default.GetTokens(expression);
            var expected = new string[] { "(", "5", "*", "2", ")", "+", "1" };

            Assert.AreEqual(7, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, result.ToStringExpression(), result.CollectionToString(s => s.Value));

            Assert.AreEqual(new Token("(", TokenType.Parenthesis), result[0]);
            Assert.AreEqual(new Token("5", TokenType.Number), result[1]);
            Assert.AreEqual(new Token("*", TokenType.BinaryOperator), result[2]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[3]);
            Assert.AreEqual(new Token(")", TokenType.Parenthesis), result[4]);
            Assert.AreEqual(new Token("+", TokenType.BinaryOperator), result[5]);
            Assert.AreEqual(new Token("1", TokenType.Number), result[6]);
        }

        [Test()]
        public void GetTokensTest7()
        {
            var expression = "5 / -2";
            var result = Tokenizer.Default.GetTokens(expression);
            var expected = new string[] { "5", "/", "-", "2" };

            Assert.AreEqual(4, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, result.ToStringExpression(), result.CollectionToString(s => s.Value));

            Assert.AreEqual(new Token("5", TokenType.Number), result[0]);
            Assert.AreEqual(new Token("/", TokenType.BinaryOperator), result[1]);
            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[2]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[3]);
        }

        [Test()]
        public void GetTokensTest8()
        {
            var expression = "5 > -2";
            var result = Tokenizer.Default.GetTokens(expression);
            var expected = new string[] { "5", ">", "-", "2" };

            Assert.AreEqual(4, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, result.ToStringExpression(), result.CollectionToString(s => s.Value));

            Assert.AreEqual(new Token("5", TokenType.Number), result[0]);
            Assert.AreEqual(new Token(">", TokenType.Unknown), result[1]);
            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[2]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[3]);
        }

        [Test()]
        public void GetTokensTest9()
        {
            var expression = "5 any2 -2".RemoveWhiteSpaces();
            var result = Tokenizer.Default.GetTokens(expression);
            var expected = new string[] { "5", "any2", "-", "2" };

            Assert.AreEqual(4, result.Length, result.CollectionToString(s => s.Value));
            CollectionAssert.AreEqual(expected, result.ToStringExpression(), result.CollectionToString(s => s.Value));

            Assert.AreEqual(new Token("5", TokenType.Number), result[0]);
            Assert.AreEqual(new Token("any2", TokenType.Unknown), result[1]);
            Assert.AreEqual(new Token("-", TokenType.UnaryOperator), result[2]);
            Assert.AreEqual(new Token("2", TokenType.Number), result[3]);
        }
    }
}