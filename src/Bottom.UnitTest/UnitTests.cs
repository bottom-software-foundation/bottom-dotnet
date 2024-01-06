using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bottom.UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [DataRow(true, "💖💖,,,,👉👈")]
        [DataRow(true, "❤️👉👈")]
        [DataRow(false, "💖✨✨✨,,,,")]
        [DataRow(false, "💖❤️👉👈")]
        [DataRow(true, "💖✨✨✨,,,,\u200B")]
        [DataRow(false, "💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B")]
        [DataRow(false, "👉👈")]
        [DataRow(false, "hello")]
        public void TestIsCharacterValueGroup(bool expectedResult, string input) => Assert.AreEqual(
            expectedResult,
            Bottomify.IsCharacterValueGroup(input)
        );


        [TestMethod]
        [DataRow(true, "💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B")]
        [DataRow(false, "💖✨✨✨,,,,")]
        [DataRow(false, "Hello")]
        [DataRow(true, "")]
        public void TestIsEncoded(bool expectedResult, string input) => Assert.AreEqual(
            expectedResult,
            Bottomify.IsEncoded(input)
        );


        [TestMethod]
        [DataRow("💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈", "Test")]
        [DataRow("💖✨✨✨,,,,👉👈💖💖,👉👈❤️👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈", "Te\0st")]
        public void TestStringEncode(string expectedResult, string input) => Assert.AreEqual(
            expectedResult,
            Bottomify.EncodeString(input)
        );


        [TestMethod]
        [DataRow("💖💖,,,,👉👈", (byte)'h')]
        [DataRow("💖✨✨✨✨🥺,,👉👈", (byte)'a')]
        [DataRow("❤️👉👈", (byte)'\0')]
        public void TestByteEncode(string expectedResult, byte input) => Assert.AreEqual(
                expectedResult,
                Bottomify.EncodeByte(input)
        );


        [TestMethod]
        [DataRow((byte)'h', "💖💖,,,,👉👈")]
        [DataRow((byte)'a', "💖✨✨✨✨,,,,,,,👉👈")]
        [DataRow((byte)'a', "💖✨✨✨✨🥺,,👉👈")]
        [DataRow((byte)'\0', "❤️👉👈")]
        public void TestCharacterValueGroupDecode(byte expectedResult, string input) => Assert.AreEqual(
            expectedResult,
            Bottomify.DecodeCharacterValueGroup(input)
        );


        [TestMethod]
        [DataRow("Test", "💖✨✨✨,,,,\u200B💖💖,\u200B💖💖✨🥺\u200B💖💖✨🥺,\u200B")]
        [DataRow("Test", "💖✨✨✨,,,,👉👈💖💖,👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈")]
        [DataRow("Te\0st", "💖✨✨✨,,,,👉👈💖💖,👉👈❤️👉👈💖💖✨🥺👉👈💖💖✨🥺,👉👈")]
        public void TestStringDecode(string expectedResult, string input) => Assert.AreEqual(
            expectedResult,
            Bottomify.DecodeString(input)
        );


        [TestMethod]
        [DataRow("🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈", "🥺")]
        [DataRow("🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" +
                 "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" +
                 "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈", "がんばれ")]
        public void TestUnicodeStringEncode(string expectedResult, string input) => Assert.AreEqual(
            expectedResult,
            Bottomify.EncodeString(input)
        );


        [TestMethod]
        [DataRow("🥺", "🫂✨✨✨✨👉👈💖💖💖🥺,,,,👉👈💖💖💖✨🥺👉👈💖💖💖✨✨✨🥺,👉👈")]
        [DataRow("がんばれ", "🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈💖💖✨✨✨✨👉👈🫂✨✨🥺,,👉👈" + 
                            "💖💖✨✨✨👉👈💖💖✨✨✨✨🥺,,👉👈🫂✨✨🥺,,👉👈💖💖✨✨🥺,,,,👉👈" + 
                            "💖💖💖✨✨🥺,👉👈🫂✨✨🥺,,👉👈💖💖✨✨✨👉👈💖💖✨✨✨✨👉👈")]

        public void TestUnicodeStringDecode(string expectedResult, string input) => Assert.AreEqual(
            expectedResult,
            Bottomify.DecodeString(input)
        );
    }
}
