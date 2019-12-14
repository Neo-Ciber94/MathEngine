using System;
using MathEngine;
using NUnit.Framework;

namespace MathEngineTests
{
    [TestFixture]
    public class OperatorsAndFunctionsTests
    {
        private const double Delta = 0.00001;

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

        [Test]
        public void ModuloTest()
        {
            var op = MathContext.Default.GetInfixFunction("mod");
            Assert.AreEqual(3, op.Evaluate(15, 4));
        }

        [Test]
        public void LogTest()
        {
            var func = MathContext.Default.GetFunction("log");
            Assert.AreEqual(Math.Log(25), func.Call(new double[] { 25 }), Delta);
        }

        [Test]
        public void SqrtTest()
        {
            var func = MathContext.Default.GetFunction("sqrt");
            Assert.AreEqual(5, func.Call(new double[] { 25 }));
        }

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
        public void SineTest()
        {
            var func = MathContext.Default.GetFunction("sin");
            Assert.AreEqual(Math.Sin(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void CosineTest()
        {
            var func = MathContext.Default.GetFunction("cos");
            Assert.AreEqual(Math.Cos(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void TangentTest()
        {
            var func = MathContext.Default.GetFunction("tan");
            Assert.AreEqual(Math.Tan(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void CosecantTest()
        {
            var func = MathContext.Default.GetFunction("csc");
            Assert.AreEqual(1 / Math.Sin(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void SecantTest()
        {
            var func = MathContext.Default.GetFunction("sec");
            Assert.AreEqual(1 / Math.Cos(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void CotangentTest()
        {
            var func = MathContext.Default.GetFunction("cot");
            Assert.AreEqual(1 / Math.Tan(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicSineTest()
        {
            var func = MathContext.Default.GetFunction("sinh");
            Assert.AreEqual(Math.Sinh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicCosineTest()
        {
            var func = MathContext.Default.GetFunction("cosh");
            Assert.AreEqual(Math.Cosh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicTangentTest()
        {
            var func = MathContext.Default.GetFunction("tanh");
            Assert.AreEqual(Math.Tanh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicCosecantTest()
        {
            var func = MathContext.Default.GetFunction("csch");
            Assert.AreEqual(1 / Math.Sinh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicSecantTest()
        {
            var func = MathContext.Default.GetFunction("sech");
            Assert.AreEqual(1 / Math.Cosh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicCotangentTest()
        {
            var func = MathContext.Default.GetFunction("coth");
            Assert.AreEqual(1 / Math.Tanh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusSineTest()
        {
            var func = MathContext.Default.GetFunction("asin");
            Assert.AreEqual(Math.Asin(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusCosineTest()
        {
            var func = MathContext.Default.GetFunction("acos");
            Assert.AreEqual(Math.Acos(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusTangentTest()
        {
            var func = MathContext.Default.GetFunction("atan");
            Assert.AreEqual(Math.Atan(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusCosecantTest()
        {
            var func = MathContext.Default.GetFunction("acsc");
            Assert.AreEqual(1 / Math.Asin(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusSecantTest()
        {
            var func = MathContext.Default.GetFunction("asec");
            Assert.AreEqual(1 / Math.Acos(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void ArcusCotangentTest()
        {
            var func = MathContext.Default.GetFunction("acot");
            Assert.AreEqual(1 / Math.Atan(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusSineTest()
        {
            var func = MathContext.Default.GetFunction("asinh");
            Assert.AreEqual(Math.Asinh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusCosineTest()
        {
            var func = MathContext.Default.GetFunction("acosh");
            Assert.AreEqual(Math.Acosh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusTangentTest()
        {
            var func = MathContext.Default.GetFunction("atanh");
            Assert.AreEqual(Math.Atanh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusCosecantTest()
        {
            var func = MathContext.Default.GetFunction("acsch");
            Assert.AreEqual(1 / Math.Asinh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusSecantTest()
        {
            var func = MathContext.Default.GetFunction("asech");
            Assert.AreEqual(1 / Math.Acosh(45), func.Call(new double[] { 45 }), Delta);
        }

        [Test]
        public void HyperbolicArcusCotangentTest()
        {
            var func = MathContext.Default.GetFunction("acoth");
            Assert.AreEqual(1 / Math.Atanh(45), func.Call(new double[] { 45 }), Delta);
        }
    }
}
