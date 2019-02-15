using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class RightTests
    {
        [Test]
        [TestCase("This is CRM", 4, " CRM")]
        [TestCase("My name is Arthur", 3, "hur")]
        public void HappyPath(string inputString, int lengthToExtract, string expectedResult)
        {
            var test = new Right();
            var result = test.GetRightString(inputString, lengthToExtract);

            Assert.AreEqual(expectedResult, result);
        }
    }
}