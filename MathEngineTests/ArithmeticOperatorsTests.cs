using System;
using NUnit.Framework;

namespace MathEngine.Tests
{
    [TestFixture]
    public class ArithmeticOperatorsTests
    {
        [Test]
        public void AddTest()
        {
            var op = MathContext.Default.GetBinaryOperator("+");
            Assert.AreEqual(10, op.Evaluate(6, 4));
        }

        [Test]
        public void SubstractTest()
        {
            var op = MathContext.Default.GetBinaryOperator("-");
            Assert.AreEqual(6, op.Evaluate(10, 4));
        }

        [Test]
        public void MultiplyTest()
        {
            var op = MathContext.Default.GetBinaryOperator("*");
            Assert.AreEqual(12, op.Evaluate(3, 4));
        }

        [Test]
        public void DivideTest()
        {
            var op = MathContext.Default.GetBinaryOperator("/");
            Assert.AreEqual(20, op.Evaluate(100, 5));
            Assert.Throws<DivideByZeroException>(() => op.Evaluate(1, 0));
        }

        [Test]
        public void PowTest()
        {
            var op = MathContext.Default.GetBinaryOperator("^");
            Assert.AreEqual(25, op.Evaluate(5, 2));
        }

        [Test]
        public void PlusTest()
        {
            var op = MathContext.Default.GetUnaryOperator("+");
            Assert.AreEqual(3, op.Evaluate(3));
        }

        [Test]
        public void MinusTest()
        {
            var op = MathContext.Default.GetUnaryOperator("-");
            Assert.AreEqual(3, op.Evaluate(-3));
            Assert.AreEqual(-6, op.Evaluate(6));
        }

        [Test]
        public void FactorialTest()
        {
            var op = MathContext.Default.GetUnaryOperator("!");
            Assert.AreEqual(720, op.Evaluate(6));
            Assert.AreEqual(1, op.Evaluate(0));
            Assert.Throws<ArithmeticException>(() => op.Evaluate(-30));
        }
    }
}
