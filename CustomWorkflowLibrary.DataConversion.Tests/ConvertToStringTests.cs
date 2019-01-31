using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using NUnit.Framework;

namespace CustomWorkflowLibrary.DataConversion.Tests
{
    [TestFixture]
    public class ConvertToStringTests
    {

        public class StringTestCase
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

            public string TestCaseName { get; set; }

            //Inputs
            public DateTime? InputDate { get; set; }
            public bool? InputBool { get; set; }
            public int? InputInt { get; set; }
            public double? InputDouble { get; set; }
            public Money InputMoney { get; set; }

            // Outputs
            public string ExpectedStringOutput { get; set; }
        }

        #region Test Cases
        private static IEnumerable<StringTestCase> AddTests()
        {
            yield return new StringTestCase(
                "DateTime Positive Test 1",
                new DateTime(2010, 7, 8),
                true,
                2,
                1.4,
                new Money(new decimal(20.41)),
                "08/07/2010"
            );

            yield return new StringTestCase(
                "DateTime Positive Test 2",
                new DateTime(1890, 4, 1),
                true,
                2,
                1.4,
                new Money(new decimal(20.41)),
                "01/04/1890"
            );

            yield return new StringTestCase(
                "Bool Positive Test 1",
                null,
                true,
                2,
                1.4,
                new Money(new decimal(20.41)),
                "True"
            );

            yield return new StringTestCase(
                "Bool Positive Test 2",
                null,
                false,
                2,
                1.4,
                new Money(new decimal(20.41)),
                "False"
            );

            yield return new StringTestCase(
                "Int Positive Test 1",
                null,
                null,
                2,
                1.4,
                new Money(new decimal(20.41)),
                "2"
            );

            yield return new StringTestCase(
                "Int Positive Test 2",
                null,
                null,
                9000,
                1.4,
                new Money(new decimal(20.41)),
                "9000"
            );

            yield return new StringTestCase(
                "Int Positive Test 3",
                null,
                null,
                -4456,
                1.4,
                new Money(new decimal(20.41)),
                "-4456"
            );

            yield return new StringTestCase(
                "Double Positive Test 1",
                null,
                null,
                null,
                1.4,
                new Money(new decimal(20.41)),
                "1.4"
            );

            yield return new StringTestCase(
                "Double Positive Test 2",
                null,
                null,
                null,
                3.14792,
                new Money(new decimal(20.41)),
                "3.14792"
            );

            yield return new StringTestCase(
                "Double Positive Test 3",
                null,
                null,
                null,
                -423.998,
                new Money(new decimal(20.41)),
                "-423.998"
            );

            yield return new StringTestCase(
                "Money Positive Test 1",
                null,
                null,
                null,
                null,
                new Money(new decimal(20.41)),
                "20.41"
            );

            yield return new StringTestCase(
                "Money Positive Test 2",
                null,
                null,
                null,
                null,
                new Money(new decimal(1447587.99)),
                "1447587.99"
            );

        }
        #endregion Test Cases

        [Test, TestCaseSource(nameof(AddTests))]
        public void HappyPath2(StringTestCase testCase)
        {
            var test = new ConvertToString();
            var result = test.ChooseConversion(testCase.InputDate, testCase.InputBool, testCase.InputInt,
                testCase.InputDouble, testCase.InputMoney);
            
            Assert.AreEqual(testCase.ExpectedStringOutput, result, testCase.TestCaseName);
        }
    }
}
