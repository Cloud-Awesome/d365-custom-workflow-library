using NUnit.Framework;

namespace DataConversion.Tests
{
    [TestFixture]
    public class ConvertToWholeNumberTests
    {
        [Test]
        [TestCase("23", 23)]
        [TestCase("-23", -23)]
        [TestCase("147", 147)]
        [TestCase("90321", 90321)]
        [TestCase("-90321", -90321)]
        public void HappyPath(string inputString, int expectedOutput)
        {
            var test = new ConvertToWholeNumber();
            var result = test.DoConversion(inputString);
            Assert.AreEqual(expectedOutput, result);
            Assert.IsInstanceOf<int>(result);
        }

        [Test]
        [TestCase("23.12")]
        [TestCase("-23.56")]
        [TestCase("147.00")]
        public void InvalidStringInput(string inputString)
        {
            var test = new ConvertToWholeNumber();
            Assert.Throws<System.FormatException>( () => test.DoConversion(inputString));
        }
    }
}
