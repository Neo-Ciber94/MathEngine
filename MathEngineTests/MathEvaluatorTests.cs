using NUnit.Framework;
using MathEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathEngine.Tests
{
    [TestFixture()]
    public class MathEvaluatorTests
    {
        [Test()]
        public void EvaluateTest1()
        {
            var expression = "10 + 5 * 2";
            var tokens = Tokenizer.GetTokens(expression);
            var rpn = MathParser.InfixToRPN(tokens);

            Assert.AreEqual(20, MathEvaluator.Evaluate(rpn));
            Assert.AreEqual(20, MathEvaluator.Evaluate(expression));
        }

        [Test()]
        public void EvaluateTest2()
        {
            var expression = "(10 + (-5 * 2))";
            var tokens = Tokenizer.GetTokens(expression);
            var rpn = MathParser.InfixToRPN(tokens);

            Assert.AreEqual(0, MathEvaluator.Evaluate(rpn));
            Assert.AreEqual(0, MathEvaluator.Evaluate(expression));
        }

        [Test()]
        public void EvaluateTest3()
        {
            var expression = "max(10 - 4, 2 + 6)";
            var tokens = Tokenizer.GetTokens(expression);
            var rpn = MathParser.InfixToRPN(tokens);

            Assert.AreEqual(8, MathEvaluator.Evaluate(rpn));
            Assert.AreEqual(8, MathEvaluator.Evaluate(expression));
        }
    }
}