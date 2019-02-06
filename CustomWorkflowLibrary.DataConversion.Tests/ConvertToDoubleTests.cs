using NUnit.Framework;

namespace CustomWorkflowLibrary.DataConversion.Tests
{
    [TestFixture]
    public class ConvertToDoubleTests
    {
        [Test]
        [TestCase("23.01", 23.01)]
        [TestCase("-23.00", -23.0)]
        [TestCase("2347.99384", 2347.99384)]
        [TestCase("-0.03321", -0.03321)]
        public void HappyPath(string doubleString, double expectedOutput)
        {
            var test = new ConvertToDouble();
            var result = test.DoConversion(doubleString);
            Assert.AreEqual(expectedOutput, result);
            Assert.IsInstanceOf<double>(result);
        }
    }
}
