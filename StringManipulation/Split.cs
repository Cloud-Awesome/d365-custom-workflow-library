using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace StringManipulation
{
    public class Split: CodeActivity
    {
        public string GetSplitOutput(string inputString, string deliminator, int returnIndex)
        {
            var splitInput = inputString.Split(deliminator.ToCharArray(0,1));
            var base0ReturnIndex = returnIndex - 1; // + 1 to raise from base 0 to base 1

            if (splitInput.Length < returnIndex)
            {
                throw new InvalidPluginExecutionException(
                    $"Return Index ({returnIndex}) is larger than the number " +
                    $"of elements in the Input String ({splitInput.Length})");
            }

            return splitInput[base0ReturnIndex];
        }

        [Input("Input String")]
        public InArgument<string> StringInArgument { get; set; }

        [Input("Deliminator")]
        public InArgument<string> DeliminatorInArgument { get; set; }

        [Input("Return Index")]
        public InArgument<int> ReturnIndexInArgument { get; set; }

        [Output("Output String")]
        public OutArgument<string> StringOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var intputString = StringInArgument.Get(context);
            var deliminator = DeliminatorInArgument.Get(context);
            var returnIndex = ReturnIndexInArgument.Get(context);

            var returnValue = GetSplitOutput(intputString, deliminator, returnIndex);

            StringOutArgument.Set(context, returnValue);
        }
    }
}