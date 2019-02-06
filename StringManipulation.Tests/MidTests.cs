using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class MidTests
    {
        [Test]
        [TestCase("Yes, Arthur is my name", 6, 5, "Arthur")]
        [TestCase("The lazy brown fox", 3, 7, "y b")]
        public void HappyPath(string inputString, int numberOfStringsToExtract, int startingInddex,
            string expectedResult)
        {
            var test = new Mid();
            var result = test.GetMidString(inputString, numberOfStringsToExtract, startingInddex);

            Assert.AreEqual(expectedResult, result);
        }
    }
}