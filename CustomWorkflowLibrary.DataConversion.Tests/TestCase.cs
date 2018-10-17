using System;
using Microsoft.Xrm.Sdk;

namespace CustomWorkflowLibrary.DataConversion.Tests
{
    public class TestCase
    {
        public TestCase(string testCaseName, string inputString, DateTime? inputDate, bool? inputBool, int? inputInt, 
            double? inputDouble, Money inputMoney, string expectedStringOutput, DateTime expectedDateTimeOutput, 
            bool expectedBoolOutput, int expectedIntOutput, double expectedDoubleOutput, Money expectedMoneyOutput)
        {
            TestCaseName = testCaseName;
            InputString = inputString;
            InputDate = inputDate;
            InputBool = inputBool;
            InputInt = inputInt;
            InputDouble = inputDouble;
            InputMoney = inputMoney;
            ExpectedStringOutput = expectedStringOutput;
            ExpectedDateTimeOutput = expectedDateTimeOutput;
            ExpectedBoolOutput = expectedBoolOutput;
            ExpectedIntOutput = expectedIntOutput;
            ExpectedDoubleOutput = expectedDoubleOutput;
            ExpectedMoneyOutput = expectedMoneyOutput;
        }

        public string TestCaseName { get; set; }
        
        //Inputs
        public string InputString { get; set; }
        public DateTime? InputDate { get; set; }
        public bool? InputBool { get; set; }
        public int? InputInt { get; set; }
        public double? InputDouble { get; set; }
        public Money InputMoney { get; set; }

        // Outputs
        public string ExpectedStringOutput { get; set; }
        public DateTime ExpectedDateTimeOutput { get; set; }
        public bool ExpectedBoolOutput { get; set; }
        public int ExpectedIntOutput { get; set; }
        public double ExpectedDoubleOutput { get; set; }
        public Money ExpectedMoneyOutput { get; set; }
            
    }
}