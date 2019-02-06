using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class LengthTests
    {
        [Test]
        [TestCase("aaa", 3)]
        [TestCase("", 0)]
        [TestCase("Hello, my name is Peter.", 24)]
        public void HappyPath(string inputString, int expectedResult)
        {
            var test = new Length();
            var result = test.GetLength(inputString);

            Assert.AreEqual(expectedResult, result);

        }
    }
}