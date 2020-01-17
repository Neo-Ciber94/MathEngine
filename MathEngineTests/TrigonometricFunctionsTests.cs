using System;
using NUnit.Framework;
using ExtraUtils.MathEngine;

namespace ExtraUtils.MathEngine.Tests
{
    [TestFixture]
    public class TrigonometricFunctionsTests
    {
        private const double DegreesToRadians = Math.PI / 180;
        private const double Delta = 0.00001;

        [Test]
        public void SineTest()
        {
            var func = MathContext.Default.GetFunction("sin");
            Assert.AreEqual(Math.Sin(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void CosineTest()
        {
            var func = MathContext.Default.GetFunction("cos");
            Assert.AreEqual(Math.Cos(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void TangentTest()
        {
            var func = MathContext.Default.GetFunction("tan");
            Assert.AreEqual(Math.Tan(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void CosecantTest()
        {
            var func = MathContext.Default.GetFunction("csc");
            Assert.AreEqual(1 / Math.Sin(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void SecantTest()
        {
            var func = MathContext.Default.GetFunction("sec");
            Assert.AreEqual(1 / Math.Cos(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void CotangentTest()
        {
            var func = MathContext.Default.GetFunction("cot");
            Assert.AreEqual(1 / Math.Tan(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicSineTest()
        {
            var func = MathContext.Default.GetFunction("sinh");
            Assert.AreEqual(Math.Sinh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicCosineTest()
        {
            var func = MathContext.Default.GetFunction("cosh");
            Assert.AreEqual(Math.Cosh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicTangentTest()
        {
            var func = MathContext.Default.GetFunction("tanh");
            Assert.AreEqual(Math.Tanh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicCosecantTest()
        {
            var func = MathContext.Default.GetFunction("csch");
            Assert.AreEqual(1 / Math.Sinh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicSecantTest()
        {
            var func = MathContext.Default.GetFunction("sech");
            Assert.AreEqual(1 / Math.Cosh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicCotangentTest()
        {
            var func = MathContext.Default.GetFunction("coth");
            Assert.AreEqual(1 / Math.Tanh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusSineTest()
        {
            var func = MathContext.Default.GetFunction("asin");
            Assert.AreEqual(Math.Asin(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusCosineTest()
        {
            var func = MathContext.Default.GetFunction("acos");
            Assert.AreEqual(Math.Acos(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusTangentTest()
        {
            var func = MathContext.Default.GetFunction("atan");
            Assert.AreEqual(Math.Atan(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusCosecantTest()
        {
            var func = MathContext.Default.GetFunction("acsc");
            Assert.AreEqual(1 / Math.Asin(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusSecantTest()
        {
            var func = MathContext.Default.GetFunction("asec");
            Assert.AreEqual(1 / Math.Acos(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusCotangentTest()
        {
            var func = MathContext.Default.GetFunction("acot");
            Assert.AreEqual(1 / Math.Atan(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusSineTest()
        {
            var func = MathContext.Default.GetFunction("asinh");
            Assert.AreEqual(Math.Asinh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusCosineTest()
        {
            var func = MathContext.Default.GetFunction("acosh");
            Assert.AreEqual(Math.Acosh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusTangentTest()
        {
            var func = MathContext.Default.GetFunction("atanh");
            Assert.AreEqual(Math.Atanh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusCosecantTest()
        {
            var func = MathContext.Default.GetFunction("acsch");
            Assert.AreEqual(1 / Math.Asinh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusSecantTest()
        {
            var func = MathContext.Default.GetFunction("asech");
            Assert.AreEqual(1 / Math.Acosh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusCotangentTest()
        {
            var func = MathContext.Default.GetFunction("acoth");
            Assert.AreEqual(1 / Math.Atanh(45 * DegreesToRadians), func.Call(new double[] { 45 }), Delta);
        }

    }
}
