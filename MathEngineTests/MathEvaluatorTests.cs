using NUnit.Framework;
using MathEngine;
using MathEngine.Utils;
using System.Linq;
using System;

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
            CollectionAssert.AreEqual(expected, rpn.TokensToString(), rpn.Select(s => s.ToString()).AsString());
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
        public void InfixToRPNTest4()
        {
            var tokens = Tokenizer.Instance.GetTokens("e^2 + 1");
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            // e 2 ^ 1 +
            var expected = new string[] { "e", "2", "^", "1", "+" };
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

        [Test()]
        public void EvaluateTest4()
        {
            var expression = "e^2 + 1";
            var tokens = Tokenizer.Instance.GetTokens(expression);
            var rpn = MathEvaluator.Instance.InfixToRPN(tokens);

            var result = Math.Exp(2) + 1;
            var delta = 0.00001;
            Assert.AreEqual(result, MathEvaluator.Instance.Evaluate(rpn), delta);
            Assert.AreEqual(result, MathEvaluator.Instance.Evaluate(expression), delta);
        }
    }
}