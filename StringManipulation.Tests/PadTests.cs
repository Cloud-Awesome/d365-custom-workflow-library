using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class PadTests
    {
        [Test]
        [TestCase("crm", 'x', false, 10, "crmxxxxxxx")]
        [TestCase("crm", 'x', true, 10, "xxxxxxxcrm")]
        [TestCase("", '~', false, 10, "~~~~~~~~~~")]
        public void HappyPath(string inputString, char padCharacter, bool padOnLeft, int finalLength,
            string expectedResult)
        {
            var test = new Pad();
            var result = test.PadString(inputString, padCharacter, padOnLeft, finalLength);

            Assert.AreEqual(expectedResult.Length, result.Length);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
