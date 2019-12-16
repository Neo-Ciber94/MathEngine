using NUnit.Framework;
using MathEngine;
using MathEngine.Utils;
using System.Linq;
using System;

namespace MathEngine.Tests
{
    [TestFixture()]
    public class MathEvaluatorRPNTests
    {
        [Test()]
        public void InfixToRPNTest1()
        {
            var expression = "10 + 5 * 2";
            var tokens = Tokenizer.Default.GetTokens(expression);
            var rpn = MathEvaluator.InfixToRPN(tokens);

            // 10 5 2 * +
            var expected = new string[] { "10", "5", "2", "*", "+" };
            CollectionAssert.AreEqual(expected, rpn.ToStringExpression(), rpn.Select(s => s.ToString()).AsString());
        }

        [Test()]
        public void InfixToRPNTest2()
        {

            var tokens = Tokenizer.Default.GetTokens("(10 + (-5 * 2))");
            var rpn = MathEvaluator.InfixToRPN(tokens);

            //10 5 - 2 * +
            var expected = new string[] { "10", "5", "-", "2", "*", "+" };
            Assert.AreEqual(expected, TokenExtensions.ToStringExpression(rpn), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }

        [Test()]
        public void InfixToRPNTest3()
        {
            var tokens = Tokenizer.Default.GetTokens("max(10 - 4, 2 + 6)");
            var rpn = MathEvaluator.InfixToRPN(tokens);

            // 10 4 - 2 6 + max
            var expected = new string[] { "10", "4", "-", "2", "6", "+", "max" };
            Assert.AreEqual(expected, TokenExtensions.ToStringExpression(rpn), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }

        [Test()]
        public void InfixToRPNTest4()
        {
            var tokens = Tokenizer.Default.GetTokens("e^2 + 1");
            var rpn = MathEvaluator.InfixToRPN(tokens);

            // e 2 ^ 1 +
            var expected = new string[] { "e", "2", "^", "1", "+" };
            Assert.AreEqual(expected, rpn.ToStringExpression(), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }

        [Test()]
        public void EvaluateTest1()
        {
            var expression = "10 + 5 * 2";
            var tokens = Tokenizer.Default.GetTokens(expression);
            var rpn = MathEvaluator.InfixToRPN(tokens);

            Assert.AreEqual(20, MathEvaluator.Evaluate(rpn));
            Assert.AreEqual(20, MathEvaluator.Evaluate(expression));
        }

        [Test()]
        public void EvaluateTest2()
        {
            var expression = "(10 + (-5 * 2))";
            var tokens = Tokenizer.Default.GetTokens(expression);
            var rpn = MathEvaluator.InfixToRPN(tokens);

            Assert.AreEqual(0, MathEvaluator.Evaluate(rpn));
            Assert.AreEqual(0, MathEvaluator.Evaluate(expression));
        }

        [Test()]
        public void EvaluateTest3()
        {
            var expression = "max(10 - 4, 2 + 6)";
            var tokens = Tokenizer.Default.GetTokens(expression);
            var rpn = MathEvaluator.InfixToRPN(tokens);

            Assert.AreEqual(8, MathEvaluator.Evaluate(rpn));
            Assert.AreEqual(8, MathEvaluator.Evaluate(expression));
        }

        [Test()]
        public void EvaluateTest4()
        {
            var expression = "e^2 + 1";
            var tokens = Tokenizer.Default.GetTokens(expression);
            var rpn = MathEvaluator.InfixToRPN(tokens);

            var result = Math.Exp(2) + 1;
            var delta = 0.00001;
            Assert.AreEqual(result, MathEvaluator.Evaluate(rpn), delta);
            Assert.AreEqual(result, MathEvaluator.Evaluate(expression), delta);
        }

        [Test()]
        public void EvaluateTest5()
        {
            var expression = "(5 * 2) + 1";
            var tokens = Tokenizer.Default.GetTokens(expression);
            var rpn = MathEvaluator.InfixToRPN(tokens);

            var expectedRPN = new string[] { "5", "2", "*", "1", "+" };
            CollectionAssert.AreEqual(expectedRPN, rpn.ToStringExpression(), rpn.ToStringExpression().AsString());

            Assert.AreEqual(11, MathEvaluator.Evaluate(expression));
            Assert.AreEqual(11, MathEvaluator.Evaluate(rpn));
        }
    }
}