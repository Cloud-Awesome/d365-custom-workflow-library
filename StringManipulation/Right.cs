using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace StringManipulation
{
    public class Right: CodeActivity
    {
        public string GetRightString(string inputString, int lengthToExtract)
        {
            return inputString.Substring(inputString.Length - lengthToExtract, lengthToExtract);
        }

        [Input("Input string")]
        public InArgument<string> InputStringInArgument { get; set; }

        [Input("Length of string to extract")]
        public InArgument<int> LengthOfStringToExtractInArgument { get; set; }

        [Output("Extracted string")]
        public OutArgument<string> OutputString { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputString = InputStringInArgument.Get(context);
            var lengthToExtract = LengthOfStringToExtractInArgument.Get(context);

            var result = GetRightString(inputString, lengthToExtract);

            OutputString.Set(context, result);
        }
    }
}