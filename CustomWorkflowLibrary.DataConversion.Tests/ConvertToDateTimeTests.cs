using System;
using Microsoft.Xrm.Sdk;
using NUnit.Framework;

namespace CustomWorkflowLibrary.DataConversion.Tests
{
    [TestFixture]
    public class ConvertToDateTimeTests
    {
        private readonly TestCase[] _tests =
        {
            /*
            new TestCase()
            {
                InputString = "08/07/2018",
                ExpectedDateTimeOutput = new DateTime(2010, 7, 8),
                TestCaseName = "Positive Test 1"
            },
            new TestCase()
            {
                InputString = "21/08/2018",
                ExpectedDateTimeOutput = new DateTime(2018, 8, 21),
                TestCaseName = "Positive Test 1"
            }
            */
        };

        [Test]
        public void HappyPath([ValueSource(nameof(_tests))]TestCase tests)
        {
            var test = new ConvertToDateTime();
            var results = test.DoConversion(tests.InputString);
            var expectedValue = tests.ExpectedDateTimeOutput;

            Assert.AreEqual(expectedValue, results);
        }
    }
}
