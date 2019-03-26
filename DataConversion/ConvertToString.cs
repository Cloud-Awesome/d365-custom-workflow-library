using System;
using System.Activities;
using System.Globalization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace DataConversion
{
    public class ConvertToString: CodeActivity
    {
        [Input("DateTime Input")]
        public InArgument<DateTime> DateTimeInArgument { get; set; }

        [Input("DateTime format (e.g. dd/MM/yyyy)")]
        public InArgument<string> DateTimeFormatInArgument { get; set; }

        [Input("or, Boolean Input")]
        public InArgument<bool> BoolInArgument { get; set; }

        [Input("or, Whole Number Input")]
        public InArgument<int> IntegerInArgument { get; set; }

        [Input("or, Decimal Number Input")]
        public InArgument<double> DoubleInArgument { get; set; }

        [Input("or, Currency Input")]
        public InArgument<Money> MoneyInArgument { get; set; }
        
        [Output("String Output")]
        public OutArgument<string> StringOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputDateTime = DateTimeInArgument.Get(context);
            var dateTimeFormat = DateTimeFormatInArgument.Get(context);
            var inputBool = BoolInArgument.Get(context);
            var inputInteger = IntegerInArgument.Get(context);
            var inputDouble = DoubleInArgument.Get(context);
            var inputMoney = MoneyInArgument.Get(context);

            var returnValue = ChooseConversion(inputDateTime, inputBool, inputInteger,
                inputDouble, inputMoney, dateTimeFormat);

            StringOutArgument.Set(context, returnValue);

        }

        public string ChooseConversion(DateTime? inputDateTime, bool? inputBool, int? inputInt,
            double? inputDouble, Money inputMoney, string dateTimeFormat)
        {
            if (inputDateTime.HasValue)
            {
                return DoConversion(inputDateTime.Value, dateTimeFormat);
            }
            if (inputBool.HasValue)
            {
                return DoConversion(inputBool.Value);
            }
            if (inputInt.HasValue)
            {
                return DoConversion(inputInt.Value);
            }
            if (inputDouble.HasValue)
            {
                return DoConversion(inputDouble.Value);
            }
            return DoConversion(inputMoney);
        }

        private string DoConversion(DateTime inputDateTime, string dateTimeFormat)
        {
            return dateTimeFormat == string.Empty ? 
                inputDateTime.ToString() : 
                inputDateTime.ToString(dateTimeFormat);
        }

        private string DoConversion(bool inputBool)
        {
            return inputBool.ToString();
        }

        private string DoConversion(int inputInt)
        {
            return inputInt.ToString();
        }

        private string DoConversion(double inputDouble)
        {
            return inputDouble.ToString(CultureInfo.CurrentCulture);
        }

        private string DoConversion(Money inputMoney)
        {
            // QUESTION: - Not sure right now how best to include a currency symbol etc.
            try
            {
                return inputMoney.Value.ToString();
            }
            catch (Exception)
            {
                throw new InvalidPluginExecutionException("Conversion of Currency input failed.");
            }
        }

    }
}
