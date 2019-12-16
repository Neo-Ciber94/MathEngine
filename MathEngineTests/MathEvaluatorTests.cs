using MathEngine;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEngineTests
{
    [TestFixture]
    public class MathEvaluatorTests
    {
        private const double Delta = 0.0001;

        [Test]
        public void Test1()
        {
            Assert.AreEqual(25, MathEvaluator.Evaluate("25"));
            Assert.AreEqual(-3, MathEvaluator.Evaluate("-3"));
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(25, MathEvaluator.Evaluate("(25)"));
            Assert.AreEqual(-3, MathEvaluator.Evaluate("(-(3))"));
            Assert.AreEqual(6, MathEvaluator.Evaluate("-(-(+6))"));

            Assert.Throws<ExpressionEvaluationException>(() => MathEvaluator.Evaluate("((3)"));
        }

        [Test]
        public void Test3()
        {
            Assert.AreEqual(25, MathEvaluator.Evaluate("(3 + 2) * 10 / 2"));
            Assert.AreEqual(453, MathEvaluator.Evaluate("3 + (3 * 5)^ 2 * 2"));
            Assert.AreEqual(121, MathEvaluator.Evaluate("( 5 + 6 )^2"));
            Assert.AreEqual(-61, MathEvaluator.Evaluate("56 - ((-2) * 4) + (-(5^3))"));
            
            Assert.AreEqual(1968.7, MathEvaluator.Evaluate("(3 ^ 9 + 2 * 3 * 4) / 10.0 - 2"), Delta);
            Assert.AreEqual(25.5 * 3.666, MathEvaluator.Evaluate("25.5 * 3.666"), Delta);
            Assert.AreEqual(21.333, MathEvaluator.Evaluate("0.333 + (5 ^ 2) - 10 / 2.5"), Delta);
        }

        [Test]
        public void Test4()
        {
            Assert.AreEqual(25, MathEvaluator.Evaluate("(3 plus 2) times 10 divided 2"));
            Assert.AreEqual(453, MathEvaluator.Evaluate("3 plus (3 times 5) pow 2 times 2"));
            Assert.AreEqual(121, MathEvaluator.Evaluate("( 5 plus 6 ) pow 2"));
            Assert.AreEqual(-61, MathEvaluator.Evaluate("56 minus ((-2) times 4) plus (-(5 pow 3))"));

            Assert.AreEqual(1968.7, MathEvaluator.Evaluate("(3 pow 9 plus 2 times 3 times 4) divided 10.0 minus 2"), Delta);
            Assert.AreEqual(25.5 * 3.666, MathEvaluator.Evaluate("25.5 times 3.666"), Delta);
            Assert.AreEqual(21.333, MathEvaluator.Evaluate("0.333 plus (5 pow 2) minus 10 divided 2.5"), Delta);
        }

        [Test]
        public void Test5()
        {
            Assert.AreEqual(-50, MathEvaluator.Evaluate("MIN(4, -5) * MAX(10, 3)"));
            Assert.AreEqual(Math.Log10(256) - (-2), MathEvaluator.Evaluate("Log(256) - (-2)"), Delta);
            Assert.AreEqual(Math.Round(0.6) * Math.Log10(25), MathEvaluator.Evaluate("Round(0.6) * Log(25)"));
        }

        [Test]
        public void Test6()
        {
            //ToRadians(50), ToDegrees(60)
        }
    }
}
