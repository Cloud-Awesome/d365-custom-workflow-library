using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace StringManipulation
{
    public class Length: CodeActivity
    {
        public int GetLength(string inputString)
        {
            return inputString.Length;
        }

        [Input("Input String")]
        public InArgument<string> InputStringInArgument { get; set; }

        [Output("Length")]
        public OutArgument<int> LengthOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputString = InputStringInArgument.Get(context);
            LengthOutArgument.Set(context, inputString);
        }
    }
}