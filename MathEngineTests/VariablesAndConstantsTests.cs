using MathEngine;
using NUnit.Framework;

namespace MathEngineTests
{
    [TestFixture]
    public class VariablesAndConstantsTests
    {
        [Test]
        public void InfinityTest()
        {
            var infinity = MathContext.Default.GetValue("infinity");
            Assert.AreEqual(double.PositiveInfinity, infinity);
            Assert.AreEqual(double.NegativeInfinity, -infinity);
        }

        [Test]
        public void CustomVariableTest()
        {
            var context = new MathContext(("x", 10), ("y", -25));
            Assert.AreEqual(10, context.GetValue("x"));
            Assert.AreEqual(-25, context.GetValue("y"));
        }
    }
}
