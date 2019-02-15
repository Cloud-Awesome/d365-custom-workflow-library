using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class TrimTests
    {
        [Test]
        [TestCase("   asd  ", "", "asd")]
        [TestCase("~~~Arthur!~~~", "~", "Arthur!")]
        public void HappyPath(string inputString, string stringToTrim, string expectedOutput)
        {
            var test = new Trim();
            var result = test.GetTrimmedString(inputString, stringToTrim);

            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        [TestCase("~~~Arthur!~~~", "~~~", "Arthur!")]
        [TestCase("----------xxx----------", "--/", "xxx")]
        public void LongerDeliminatorsAreHandled(string inputString, string stringToTrim, string expectedOutput)
        {
            var test = new Trim();
            var result = test.GetTrimmedString(inputString, stringToTrim);

            Assert.AreEqual(expectedOutput, result);
        }
    }
}