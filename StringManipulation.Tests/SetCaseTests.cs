using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class SetCaseTests
    {
        [Test]
        [TestCase("arthur nicholson", "Proper", "Arthur Nicholson")]
        [TestCase("i want to shout this", "UPPER", "I WANT TO SHOUT THIS")]
        [TestCase("I WANT TO whisper THIS", "lower", "i want to whisper this")]
        public void HappyPath(string inputString, string caseOption, string expectedResult)
        {
            var test = new SetCase();
            var result = test.GetSetCase(inputString, caseOption);

            Assert.AreEqual(expectedResult, result);
        }
    }
}