using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace DataConversion
{
    public class ConvertToDouble: CodeActivity
    {
        [Input("String of Whole Number input")]
        public InArgument<string> StringInArgument { get; set; }

        [Output("Double Output")]
        public OutArgument<int> DoubleOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputString = StringInArgument.Get(context);
            var returnValue = DoConversion(inputString);
            DoubleOutArgument.Set(context, returnValue);
        }

        public double DoConversion(string inputString)
        {
            var returnValue = double.Parse(inputString);
            return returnValue;
        }
    }
}
