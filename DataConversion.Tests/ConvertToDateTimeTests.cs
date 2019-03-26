using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace DataConversion.Tests
{
    [TestFixture]
    public class ConvertToDateTimeTests
    {
        public class DateTimeTestCase
        {
            public DateTimeTestCase(string testCaseName, string inputString, DateTime expectedDateTimeOutput)
            {
                TestCaseName = testCaseName;
                InputString = inputString;
                ExpectedDateTimeOutput = expectedDateTimeOutput;
            }

            public string TestCaseName { get; set; }

            // Inputs
            public string InputString { get; set; }

            // Outputs
            public DateTime ExpectedDateTimeOutput { get; set; }
        }

        private static IEnumerable<DateTimeTestCase> AddTests()
        {
            yield return new DateTimeTestCase(
                "Positive Test 1",
                "08/07/2010",
                new DateTime(2010, 7, 8)                
            );

            yield return new DateTimeTestCase(
                "Positive Test 1",
                "21/08/2018",
                new DateTime(2018, 8, 21)
            );
        }

        [Test]
        public void HappyPath([ValueSource(nameof(AddTests))]DateTimeTestCase tests)
        {
            var test = new ConvertToDateTime();
            var results = test.DoConversion(tests.InputString);
            var expectedValue = tests.ExpectedDateTimeOutput;

            Assert.AreEqual(expectedValue, results);
        }
    }
}
