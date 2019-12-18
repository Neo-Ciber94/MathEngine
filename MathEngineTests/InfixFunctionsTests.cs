using NUnit.Framework;

namespace MathEngine.Tests
{
    [TestFixture]
    public class InfixFunctionsTests
    {
        [Test]
        public void PlusTest()
        {
            var op = MathContext.Default.GetInfixFunction("plus");
            Assert.AreEqual(10, op.Evaluate(6, 4));
        }

        [Test]
        public void MinusTest()
        {
            var op = MathContext.Default.GetInfixFunction("minus");
            Assert.AreEqual(-6, op.Evaluate(4, 10));
        }

        [Test]
        public void TimesTest()
        {
            var op = MathContext.Default.GetInfixFunction("times");
            Assert.AreEqual(30, op.Evaluate(10, 3));
        }

        [Test]
        public void DividedTest()
        {
            var op = MathContext.Default.GetInfixFunction("divided");
            Assert.AreEqual(3, op.Evaluate(12, 4));
        }

        [Test]
        public void PowTest()
        {
            var op = MathContext.Default.GetInfixFunction("pow");
            Assert.AreEqual(25, op.Evaluate(5, 2));
        }

        [Test]
        public void ModuloTest()
        {
            var op = MathContext.Default.GetInfixFunction("mod");
            Assert.AreEqual(3, op.Evaluate(15, 4));
        }
    }
}
