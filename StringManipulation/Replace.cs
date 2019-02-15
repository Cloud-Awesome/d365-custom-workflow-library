using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace StringManipulation
{
    public class Replace: CodeActivity
    {
        public string GetReplacedString(string inputString, string oldString, string newString)
        {
            return inputString.Replace(oldString, newString);
        }

        [Input("Original String")]
        [RequiredArgument]
        public InArgument<string> OriginalStringInArgument { get; set; }

        [Input("String to Find")]
        [RequiredArgument]
        public InArgument<string> OldStringInArgument { get; set; }

        [Input("Replace with")]
        [RequiredArgument]
        public InArgument<string> NewStringInArgument { get; set; }

        [Output("Replaced String")]
        public OutArgument<string> ReplacedStringOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var originalString = OriginalStringInArgument.Get(context);
            var oldString = OldStringInArgument.Get(context);
            var newString = NewStringInArgument.Get(context);

            var result = GetReplacedString(originalString, oldString, newString);

            ReplacedStringOutArgument.Set(context, result);
        }
    }
}