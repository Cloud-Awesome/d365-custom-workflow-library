using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace StringManipulation
{
    public class Trim: CodeActivity
    {
        public string GetTrimmedString(string inputString, string stringToTrim)
        {
            var returnValue = !string.IsNullOrEmpty(stringToTrim) ? 
                inputString.Trim(stringToTrim.ToCharArray(0,1)) : 
                inputString.Trim();

            return returnValue;
        }

        [Input("Input String")]
        [RequiredArgument]
        public InArgument<string> StringInArgument { get; set; }

        [Input("Char to Trim (leave blank to trim space)")]
        public InArgument<string> StringToTrimInArgument { get; set; }

        [Output("Trimmed String")]
        public OutArgument<string> TrimmedStringOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputString = StringInArgument.Get(context);
            var stringToTrim = StringToTrimInArgument.Get(context);

            var returnValue = GetTrimmedString(inputString, stringToTrim);

            TrimmedStringOutArgument.Set(context, returnValue);
        }
    }
}