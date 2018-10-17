using Microsoft.Xrm.Sdk;
using NUnit.Framework;

namespace CustomWorkflowLibrary.DataConversion.Tests
{
    [TestFixture]
    public class ConvertToBooleanTests
    {
        [Test]
        [TestCase("Yes", null, true)]
        [TestCase("No", null, false)]
        [TestCase(null, 0, false)]
        [TestCase("t", 0, true)]
        [TestCase("f", null, false)]
        [TestCase("1", null, true)]
        [TestCase("0", null, false)]
        public void HappyPath(string inputString, int? inputInt, bool expectedOutput)
        {
            var testClass = new ConvertToBoolean();
            var result = testClass.DoConversion(inputString, inputInt);
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        [TestCase("Yes", 0, true)]
        [TestCase("N", 1, false)]
        public void IfBothInputsArePopulatedStringWins(string inputString, int? inputInt, bool expectedOutput)
        {
            var testClass = new ConvertToBoolean();
            var result = testClass.DoConversion(inputString, inputInt);
            Assert.AreEqual(expectedOutput, result);
        }

        [Test]
        [TestCase(null, null)]
        public void InvalidInputThrowsCorrectException(string inputString, int? inputInt)
        {
            var testClass = new ConvertToBoolean();
            Assert.Throws<InvalidPluginExecutionException>(() => testClass.DoConversion(inputString, inputInt));
        }
    }
}
