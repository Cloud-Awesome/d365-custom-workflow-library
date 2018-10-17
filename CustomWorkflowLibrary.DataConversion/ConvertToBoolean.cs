using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CustomWorkflowLibrary.DataConversion
{
    public class ConvertToBoolean: CodeActivity
    {
        [Input("String Input")]
        public InArgument<string> StringInArgument { get; set; }

        [Input("or, Integer Input")]
        public InArgument<int> IntegerInArgument { get; set; }

        [Output("Boolean Output")]
        public OutArgument<bool> BooleanOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputString = StringInArgument.Get(context);
            var inputInteger = IntegerInArgument.Get(context);

            var returnValue = DoConversion(inputString, inputInteger);

            BooleanOutArgument.Set(context, returnValue);
        }

        public bool DoConversion(string inputString, int? inputInteger)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                switch (inputString.ToLower())
                {
                    case "yes":
                    case "y":
                    case "true":
                    case "t":
                    case "1":
                        return true;
                    case "no":
                    case "n":
                    case "false":
                    case "f":
                    case "0":
                        return false;
                }
            } else if (inputInteger.HasValue)
            {
                switch (inputInteger)
                {
                    case 1:
                        return true;
                    case 0:
                        return false;
                }
            }
            else
            {
                throw new InvalidPluginExecutionException("Cannot convert to bool - Neither a valid string or integer value was provided");
            }

            return false;
        }
    }
}
