using System;
using MathEngine;
using NUnit.Framework;

namespace MathEngineTests
{
    [TestFixture]
    public class SpecialFunctionsTests
    {
        [Test]
        public void MaxTest()
        {
            var func = MathContext.Default.GetFunction("max");
            Assert.AreEqual(30, func.Call(new double[] { 14, 30 }));
        }

        [Test]
        public void MinTest()
        {
            var func = MathContext.Default.GetFunction("min");
            Assert.AreEqual(14, func.Call(new double[] { 14, 30 }));
        }

        [Test]
        public void SumTest()
        {
            var func = MathContext.Default.GetFunction("sum");
            Assert.AreEqual(10, func.Call(new double[] { 1, 2, 3, 4 }));
        }

        [Test]
        [Repeat(10)]
        public void RandomTest()
        {
            var func = MathContext.Default.GetFunction("random");
            Assert.AreEqual(0, func.Call(ReadOnlySpan<double>.Empty), 1);
            Assert.AreEqual(0, func.Call(new double[] { 10 }), 10);
            Assert.AreEqual(10, func.Call(new double[] { 10, 20 }), 10);
        }

        [Test]
        public void CeilTest()
        {
            var func = MathContext.Default.GetFunction("ceil");
            Assert.AreEqual(2, func.Call(new double[] { 1.4 }));
            Assert.AreEqual(1, func.Call(new double[] { 0.7 }));
        }

        [Test]
        public void FloorTest()
        {
            var func = MathContext.Default.GetFunction("floor");
            Assert.AreEqual(1, func.Call(new double[] { 1.4 }));
            Assert.AreEqual(0, func.Call(new double[] { 0.7 }));
        }

        [Test]
        public void TruncateTest()
        {
            var func = MathContext.Default.GetFunction("truncate");
            Assert.AreEqual(1, func.Call(new double[] { 1.4 }));
            Assert.AreEqual(0, func.Call(new double[] { 0.7 }));
        }

        [Test]
        public void RoundTest()
        {
            var func = MathContext.Default.GetFunction("round");
            Assert.AreEqual(1, func.Call(new double[] { 1.4 }));
            Assert.AreEqual(1, func.Call(new double[] { 0.7 }));
        }
    }
}
