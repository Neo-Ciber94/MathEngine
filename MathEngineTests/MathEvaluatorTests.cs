using NUnit.Framework;
using MathEngine;
using MathEngine.Utils;

namespace MathEngine.Tests
{
    [TestFixture()]
    public class MathEvaluatorTests
    {
        [Test()]
        public void InfixToRPNTest1()
        {
            var expression = "10 + 5 * 2";
            var tokens = Tokenizer.Instance.GetTokens(expression);
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            // 10 5 2 * +
            var expected = new string[] { "10", "5", "2", "*", "+" };
            CollectionAssert.AreEqual(expected, rpn.TokensToString(), rpn.ToString(s => s.Value));
        }

        [Test()]
        public void InfixToRPNTest2()
        {

            var tokens = Tokenizer.Instance.GetTokens("(10 + (-5 * 2))");
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            //10 5 - 2 * +
            var expected = new string[] { "10", "5", "-", "2", "*", "+" };
            Assert.AreEqual(expected, rpn.TokensToString(), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }

        [Test()]
        public void InfixToRPNTest3()
        {
            var tokens = Tokenizer.Instance.GetTokens("max(10 - 4, 2 + 6)");
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            // 10 4 - 2 6 + max
            var expected = new string[] { "10", "4", "-", "2", "6", "+", "max" };
            Assert.AreEqual(expected, rpn.TokensToString(), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }

        [Test()]
        public void EvaluateTest1()
        {
            var expression = "10 + 5 * 2";
            var tokens = Tokenizer.Instance.GetTokens(expression);
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            Assert.AreEqual(20, MathEvaluator.Instance.Evaluate(rpn));
            Assert.AreEqual(20, MathEvaluator.Instance.Evaluate(expression));
        }

        [Test()]
        public void EvaluateTest2()
        {
            var expression = "(10 + (-5 * 2))";
            var tokens = Tokenizer.Instance.GetTokens(expression);
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            Assert.AreEqual(0, MathEvaluator.Instance.Evaluate(rpn));
            Assert.AreEqual(0, MathEvaluator.Instance.Evaluate(expression));
        }

        [Test()]
        public void EvaluateTest3()
        {
            var expression = "max(10 - 4, 2 + 6)";
            var tokens = Tokenizer.Instance.GetTokens(expression);
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            Assert.AreEqual(8, MathEvaluator.Instance.Evaluate(rpn));
            Assert.AreEqual(8, MathEvaluator.Instance.Evaluate(expression));
        }
    }
}