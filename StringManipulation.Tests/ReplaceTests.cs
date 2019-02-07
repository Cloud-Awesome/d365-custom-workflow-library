using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class ReplaceTests
    {
        [Test]
        [TestCase("Replace String", "Replace", "Replaced", "Replaced String")]
        [TestCase("Replace String", "...", "Nothing", "Replace String")]
        public void HappyPath(string inputString, string oldString, string newString, string expectedResult)
        {
            var test = new Replace();
            var result = test.GetReplacedString(inputString, oldString, newString);

            Assert.AreEqual(expectedResult, result);
        }
    }
}