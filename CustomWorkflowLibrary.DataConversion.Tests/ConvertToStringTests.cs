using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using NUnit.Framework;

namespace CustomWorkflowLibrary.DataConversion.Tests
{
    [TestFixture]
    public class ConvertToStringTests
    {

        private class StringTestCase
        {
            public StringTestCase(string testCaseName, DateTime? inputDate, bool? inputBool, int? inputInt, double? inputDouble, Money inputMoney, string expectedStringOutput)
            {
                TestCaseName = testCaseName;
                InputDate = inputDate;
                InputBool = inputBool;
                InputInt = inputInt;
                InputDouble = inputDouble;
                InputMoney = inputMoney;
                ExpectedStringOutput = expectedStringOutput;
            }

            private string TestCaseName { get; set; }

            //Inputs
            private DateTime? InputDate { get; set; }
            private bool? InputBool { get; set; }
            private int? InputInt { get; set; }
            private double? InputDouble { get; set; }
            private Money InputMoney { get; set; }

            // Outputs
            private string ExpectedStringOutput { get; set; }
        }

        #region Test Cases
        private static IEnumerable<StringTestCase> AddTests()
        {
            /*
            yield return new TestCase()
            {
                InputDate = new DateTime(1890, 4, 1),
                InputBool = true,
                InputInt = 2,
                InputDouble = 1.4,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "01/04/1890",
                TestCaseName = "DateTime Positive Test 2"
            };

            yield return new TestCase()
            {
                InputDate = null,
                InputBool = true,
                InputInt = 2,
                InputDouble = 1.4,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "True",
                TestCaseName = "Bool Positive Test 1"
            };
            yield return new TestCase()
            {
                InputDate = null,
                InputBool = false,
                InputInt = 2,
                InputDouble = 1.4,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "False",
                TestCaseName = "Bool Positive Test 2"
            };

            yield return new TestCase()
            {
                InputDate = null,
                InputBool = null,
                InputInt = 2,
                InputDouble = 1.4,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "2",
                TestCaseName = "Int Positive Test 1"
            };
            yield return new TestCase()
            {
                InputDate = null,
                InputBool = null,
                InputInt = 9000,
                InputDouble = 1.4,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "9000",
                TestCaseName = "Int Positive Test 2"
            };
            yield return new TestCase()
            {
                InputDate = null,
                InputBool = null,
                InputInt = -4456,
                InputDouble = 1.4,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "-4456",
                TestCaseName = "Int Positive Test 3"
            };

            yield return new TestCase()
            {
                InputDate = null,
                InputBool = null,
                InputInt = null,
                InputDouble = 1.4,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "1.4",
                TestCaseName = "Double Positive Test 1"
            };
            yield return new TestCase()
            {
                InputDate = null,
                InputBool = null,
                InputInt = null,
                InputDouble = 3.14792,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "3.14792",
                TestCaseName = "Double Positive Test 2"
            };
            yield return new TestCase()
            {
                InputDate = null,
                InputBool = null,
                InputInt = null,
                InputDouble = -423.998,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "-423.998",
                TestCaseName = "Double Positive Test 3"
            };

            yield return new TestCase()
            {
                InputDate = null,
                InputBool = null,
                InputInt = null,
                InputDouble = null,
                InputMoney = new Money(new decimal(20.41)),
                ExpectedStringOutput = "20.41",
                TestCaseName = "Money Positive Test 1"
            };
            yield return new TestCase(
                testCaseName: "Money Positive Test 2",
                inputString: null,
                inputDate: null,
                inputBool: null,
                inputInt: null,
                inputDouble: null,
                inputMoney: new Money(new decimal(1447587.99)),
                expectedStringOutput: "1447587.99");
            */

            yield return new StringTestCase(
                "DateTime Positive Test 1",
                new DateTime(2010, 7, 8),
                true,
                2,
                1.4,
                new Money(new decimal(20.41)),
                "08/07/2010"
            );
        }
        #endregion Test Cases

        [Test, TestCaseSource(nameof(AddTests))]
        public void HappyPath(string testCaseName, DateTime inputDate, bool inputBool, int inputInt, double inputDouble, 
            Money inputMoney, string expectedOutput)
        {
            var test = new ConvertToString();
            var result = test.ChooseConversion(inputDate, inputBool, inputInt,
                inputDouble, inputMoney);

            Assert.AreEqual(expectedOutput, result, testCaseName);
        }

        /*
        [Test]
        public void HappyPath([ValueSource(nameof(_tests))]TestCase tests)
        {
            var test = new ConvertToString();
            var result = test.ChooseConversion(tests.InputDate, tests.InputBool, tests.InputInt, 
                tests.InputDouble, tests.InputMoney);

            Assert.AreEqual(tests.ExpectedStringOutput, result, tests.TestCaseName);
        }
        */
    }
}
