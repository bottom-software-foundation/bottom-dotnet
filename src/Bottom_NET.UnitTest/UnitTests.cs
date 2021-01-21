using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bottom_NET;

namespace Bottom_NET.UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestStringEncode()
        {
            Assert.AreEqual(
                Bottom.encode_string("Test"),
                "💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈"
            );
        }

        [TestMethod]
        public void TestByteEncode()
        {
            Assert.AreEqual(
                Bottom.encode_byte((byte)'h'),
                "💖💖,,,,👉👈"
            );
        }

        [TestMethod]
        public void TestByteDecode()
        {
            Assert.AreEqual(
                Bottom.decode_byte("💖💖,,,,"),
                (byte)'h'
            );
        }

        [TestMethod]
        public void TestStringDecode()
        {
            Assert.AreEqual(
                Bottom.decode_string("💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B"),
                "Test"
            );
            Assert.AreEqual(
                Bottom.decode_string("💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈"),
                "Test"
            );
        }

        [TestMethod]
        public void TestUnicodeStringEncode()
        {
            Assert.AreEqual(
                Bottom.encode_string("🥺"),
                "🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈"
            );
            Assert.AreEqual(
                Bottom.encode_string("がんばれ"),
                "🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" +
                "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" +
                "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈"
            );
        }

        [TestMethod]
        public void TestUnicodeStringDecode()
        {
            Assert.AreEqual(
                Bottom.decode_string("🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈"),
                "🥺"
            );
            Assert.AreEqual(
                Bottom.decode_string(
                    "🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" +
                    "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" +
                    "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈"
                ),
                "がんばれ"
            );
        }
    }
}
