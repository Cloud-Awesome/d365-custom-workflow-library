using NUnit.Framework;
using Microsoft.Xrm.Sdk;

namespace StringManipulation.Tests
{
    [TestFixture]
    public class SplitTests
    {
        [Test]
        [TestCase("1;2;3;4", ";", 2, "2")]
        [TestCase("One-Two-Three-Four", "-", 4, "Four")]
        [TestCase("123.32,7746,-9938,7738,123,3321,77564", ",", 3, "-9938")]
        public void HappyPath(string inputString, string deliminator, int returnIndex, 
            string expectedResult)
        {
            var test = new Split();
            var result = test.GetSplitOutput(inputString, deliminator, returnIndex);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("1:2:3:4", "::", 2, "2")]
        public void LongerDeliminatorsAreHandled(string inputString, string deliminator, int returnIndex,
            string expectedResult)
        {
            var test = new Split();
            var result = test.GetSplitOutput(inputString, deliminator, returnIndex);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("1; 2; 3", ";", 6)]
        public void OutOfBoundsErrorIsThrown(string inputString, string deliminator, int returnIndex)
        {
            var test = new Split();
            
            Assert.Throws<InvalidPluginExecutionException>(
                () => test.GetSplitOutput(inputString, deliminator, returnIndex));
        }
    }
}