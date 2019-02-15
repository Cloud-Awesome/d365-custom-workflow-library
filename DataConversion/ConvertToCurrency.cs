using System;
using System.Activities;
using System.Globalization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace DataConversion
{
    public class ConvertToCurrency: CodeActivity
    {
        [Input("String Input")]
        public InArgument<string> StringInArgument { get; set; }

        [Input("or, Decimal Number Input")]
        public InArgument<double> DecimalInArgument { get; set; } 

        [Input("or, Whole Number Input")]
        public InArgument<int> WholeNumberInArgument { get; set; }

        [Output("Currency Output")]
        public OutArgument<Money> MoneyOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var stringInput = StringInArgument.Get(context);
            var decimalInput = DecimalInArgument.Get(context);
            var wholeNumberInput = WholeNumberInArgument.Get(context);

            var returnValue = ChooseConversion(stringInput, decimalInput, wholeNumberInput);

            MoneyOutArgument.Set(context, returnValue);

        }

        public Money ChooseConversion(string inputString, double? inputDecimal, int? inputWholeNumber)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return DoConversion(inputString);
            }
            if (inputDecimal.HasValue)
            {
                return DoConversion(inputDecimal);
            }
            return DoConversion(inputWholeNumber);
        }

        private Money DoConversion(string inputString)
        {
            var decimalValue = decimal.Parse(inputString);
            return new Money(decimalValue);
        }

        private Money DoConversion(double? inputDecimal)
        {
            var decimalValue = Convert.ToDecimal(inputDecimal);
            return new Money(decimalValue);
        }

        private Money DoConversion(int? inputInt)
        {
            var decimalValue = Convert.ToDecimal(inputInt);
            return new Money(decimalValue);
        }

    }
}
