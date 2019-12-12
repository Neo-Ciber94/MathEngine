using NUnit.Framework;
using MathEngine;
using System;
using System.Collections.Generic;
using System.Text;
using MathEngine.Utils;
using System.Linq;

namespace MathEngine.Tests
{
    [TestFixture()]
    public class MathParserTest
    {
        [Test()]
        public void InfixToRPNTest1()
        {
            var expression = "10 + 5 * 2";
            var tokens = Tokenizer.GetTokens(expression);
            var rpn = MathParser.InfixToRPN(tokens);
            var expected = new string[] { "10", "5", "2", "*", "+" };

            Assert.AreEqual(expected, rpn.TokensToString(), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }

        [Test()]
        public void InfixToRPNTest2()
        {
            var tokens = Tokenizer.GetTokens("(10 + (-5 * 2))");
            var rpn = MathParser.InfixToRPN(tokens);

            //10 5 - 2 * +
            var expected = new string[]{ "10", "5", "-", "2", "*", "+" };

            Assert.AreEqual(expected, rpn.TokensToString(), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }

        [Test()]
        public void InfixToRPNTest3()
        {
            var tokens = Tokenizer.GetTokens("max(10 - 4, 2 + 3)");
            var rpn = MathParser.InfixToRPN(tokens);

            var expected = new string[] { "10", "4", "-", "2", "3", "+", "max" };

            Assert.AreEqual(expected, rpn.TokensToString(), "{0}\n{1}", rpn.CollectionToString(s => s.Value), rpn.CollectionToString());
        }
    }
}