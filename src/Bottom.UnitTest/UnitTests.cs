using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bottom.UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestStringEncode()
        {
            Assert.AreEqual(
                Bottomify.encode_string("Test"),
                "💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈"
            );
        }

        [TestMethod]
        public void TestByteEncode()
        {
            Assert.AreEqual(
                Bottomify.encode_byte((byte)'h'),
                "💖💖,,,,👉👈"
            );
        }

        [TestMethod]
        public void TestByteDecode()
        {
            Assert.AreEqual(
                Bottomify.decode_byte("💖💖,,,,"),
                (byte)'h'
            );
        }

        [TestMethod]
        public void TestStringDecode()
        {
            Assert.AreEqual(
                Bottomify.decode_string("💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B"),
                "Test"
            );
            Assert.AreEqual(
                Bottomify.decode_string("💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈"),
                "Test"
            );
        }

        [TestMethod]
        public void TestUnicodeStringEncode()
        {
            Assert.AreEqual(
                Bottomify.encode_string("🥺"),
                "🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈"
            );
            Assert.AreEqual(
                Bottomify.encode_string("がんばれ"),
                "🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" +
                "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" +
                "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈"
            );
        }

        [TestMethod]
        public void TestUnicodeStringDecode()
        {
            Assert.AreEqual(
                Bottomify.decode_string("🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈"),
                "🥺"
            );
            Assert.AreEqual(
                Bottomify.decode_string(
                    "🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" +
                    "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" +
                    "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈"
                ),
                "がんばれ"
            );
        }
    }
}
