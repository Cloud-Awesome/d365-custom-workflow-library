using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using NUnit.Framework;

namespace DataConversion.Tests
{
    [TestFixture]
    public class ConvertToCurrencyTests
    {
        [Test]
        [TestCase("23.44", null, null, "23.44")]
        [TestCase("-234.12", null, null, "-234.12")]
        [TestCase(null, 23.44, null, "23.44")]
        [TestCase(null, -1123.99, null, "-1123.99")]
        [TestCase(null, null, 23, "23.00")]
        [TestCase(null, null, -9776, "-9776.00")]
        public void HappyPath(string inputString, double? inputDouble, int? inputWholeNumber, 
            decimal expectedResult)
        {
            var test = new ConvertToCurrency();
            var result = test.ChooseConversion(inputString, inputDouble, inputWholeNumber);
            var money = new Money(expectedResult);

            Assert.AreEqual(money, result);
            Assert.IsInstanceOf<Money>(result);
        }
    }
}
