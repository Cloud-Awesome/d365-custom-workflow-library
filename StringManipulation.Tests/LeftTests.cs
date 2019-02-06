using NUnit.Framework;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class LeftTests
    {
        [Test]
        [TestCase("This is CRM", 4, "This")]
        [TestCase("My name is Arthur", 5, "My na")]
        public void HappyPath(string inputString, int lengthToExtract, string expectedResult)
        {
            var test = new Left();
            var result = test.GetLeftString(inputString, lengthToExtract);

            Assert.AreEqual(expectedResult, result);
        }
    }
}