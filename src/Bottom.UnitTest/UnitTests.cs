using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bottom.UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestIsCharacterValueGroup()
        {
            Assert.AreEqual(
                true,
                Bottomify.IsCharacterValueGroup("💖💖,,,,👉👈")
            );
            Assert.AreEqual(
                false,
                Bottomify.IsCharacterValueGroup("💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B")
            );
            Assert.AreEqual(
                true,
                Bottomify.IsCharacterValueGroup("hello")
            );
        }

        [TestMethod]
        public void TestIsEncoded()
        {
            Assert.AreEqual(
                true,
                Bottomify.IsEncoded("💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B")
            );
            Assert.AreEqual(
                false,
                Bottomify.IsEncoded("Hello")
            );
        }

        [TestMethod]
        public void TestStringEncode()
        {
            Assert.AreEqual(
                "💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈",
                Bottomify.EncodeString("Test")
            );
            Assert.AreEqual(
                "💖✨✨✨,,,,👉👈💖💖,👉👈❤️👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈",
                Bottomify.EncodeString("Te\0st")
            );
        }

        [TestMethod]
        public void TestByteEncode()
        {
            Assert.AreEqual(
                "💖💖,,,,👉👈",
                Bottomify.EncodeByte((byte)'h')
            );
            Assert.AreEqual(
                "❤️👉👈",
                Bottomify.EncodeByte((byte)'\0')
            );
        }

        [TestMethod]
        public void TestCharacterValueGroupDecode()
        {
            Assert.AreEqual(
                (byte)'h',
                 Bottomify.DecodeCharacterValueGroup("💖💖,,,,👉👈")
            );
            Assert.AreEqual(
                (byte)'a',
                Bottomify.DecodeCharacterValueGroup("💖✨✨✨✨,,,,,,,👉👈")
            );
            Assert.AreEqual(
                (byte)'\0',
                Bottomify.DecodeCharacterValueGroup("❤️👉👈")
            );
        }

        [TestMethod]
        public void TestStringDecode()
        {
            Assert.AreEqual(
                "Test",
                Bottomify.DecodeString("💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B")
            );
            Assert.AreEqual(
                "Test",
                Bottomify.DecodeString("💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈")
            );
            Assert.AreEqual(
                "Te\0st",
                Bottomify.DecodeString("💖✨✨✨,,,,👉👈💖💖,👉👈❤️👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈")
            );
        }

        [TestMethod]
        public void TestUnicodeStringEncode()
        {
            Assert.AreEqual(
                "🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈",
                Bottomify.EncodeString("🥺")
            );
            Assert.AreEqual(
                "🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" +
                "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" +
                "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈",
                Bottomify.EncodeString("がんばれ")
            );
        }

        [TestMethod]
        public void TestUnicodeStringDecode()
        {
            Assert.AreEqual(
                "🥺",
                Bottomify.DecodeString("🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈")
            );
            Assert.AreEqual(
                "がんばれ",
                Bottomify.DecodeString(
                    "🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" +
                    "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" +
                    "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈"
                )
            );
        }
    }
}
