using NUnit.Framework;

namespace ExtraUtils.MathEngine.Utilities.Tests
{
    [TestFixture()]
    public class StringScannerTests
    {
        [Test()]
        public void ReadTest()
        {
            ValueStringReader sc = new ValueStringReader("Hello");

            Assert.IsTrue(sc.HasNext);

            Assert.AreEqual('H', sc.Read());
            Assert.AreEqual('e', sc.Read());
            Assert.AreEqual('l', sc.Read());
            Assert.AreEqual('l', sc.Read());
            Assert.AreEqual('o', sc.Read());

            Assert.IsFalse(sc.HasNext);
        }

        [Test()]
        public void CurrentTest()
        {
            ValueStringReader sc = new ValueStringReader("Hello");
            Assert.IsNull(sc.Current);

            sc.Read();
            Assert.AreEqual('H', sc.Current);

            sc.Read();
            Assert.AreEqual('e', sc.Current);

            sc.Read();
            Assert.AreEqual('l', sc.Current);

            sc.Read();
            Assert.AreEqual('l', sc.Current);

            sc.Read();
            Assert.AreEqual('o', sc.Current);

            sc.Read();
            Assert.AreEqual('o', sc.Current);
        }

        [Test()]
        public void PrevTest()
        {
            ValueStringReader sc = new ValueStringReader("Hello");
            Assert.IsNull(sc.Prev);

            sc.Read();
            Assert.IsNull(sc.Prev);

            sc.Read();
            Assert.AreEqual('H', sc.Prev);

            sc.Read();
            Assert.AreEqual('e', sc.Prev);

            sc.Read();
            Assert.AreEqual('l', sc.Prev);

            sc.Read();
            Assert.AreEqual('l', sc.Prev);

            sc.Read();
            Assert.AreEqual('l', sc.Prev);
            Assert.AreEqual('o', sc.Current);

            Assert.IsFalse(sc.HasNext);
        }

        [Test()]
        public void NextTest()
        {
            ValueStringReader sc = new ValueStringReader("Hello");

            Assert.AreEqual('H', sc.Next);
            sc.Read();

            Assert.AreEqual('e', sc.Next);
            sc.Read();

            Assert.AreEqual('l', sc.Next);
            sc.Read();

            Assert.AreEqual('l', sc.Next);
            sc.Read();

            Assert.AreEqual('o', sc.Next);
            sc.Read();

            Assert.IsNull(sc.Next);
        }
    }
}