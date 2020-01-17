using NUnit.Framework;
using ExtraUtils.MathEngine;

namespace ExtraUtils.MathEngine.Tests
{
    [TestFixture]
    public class ConstantsAndValuesTests
    {
        [Test]
        public void InfinityTest()
        {
            var infinity = MathContext.Default.GetVariableOrConstant("infinity");
            Assert.AreEqual(double.PositiveInfinity, infinity);
            Assert.AreEqual(double.NegativeInfinity, -infinity);
        }

        [Test]
        public void CustomVariableTest()
        {
            var context = new MathContext(("x", 10), ("y", -25));
            Assert.AreEqual(10, context.GetVariableOrConstant("x"));
            Assert.AreEqual(-25, context.GetVariableOrConstant("y"));
        }
    }
}
