using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace DataConversion
{
    public class ConvertToWholeNumber: CodeActivity
    {
        [Input("String of Whole Number input")]
        public InArgument<string> StringInArgument { get; set; }

        [Output("Whole Number Output")]
        public OutArgument<int> IntOutArgument { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            var inputString = StringInArgument.Get(context);
            var returnValue = DoConversion(inputString);
            IntOutArgument.Set(context, returnValue);
        }

        public int DoConversion(string inputString)
        {
            var returnValue = int.Parse(inputString);
            return returnValue;
        }
    }
}
