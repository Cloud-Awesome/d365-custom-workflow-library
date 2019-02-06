using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace StringManipulation
{
    public class Pad : CodeActivity
    {
        [Input("Input String")]
        [RequiredArgument]
        public InArgument<string> InputString { get; set; }

        [Input("Pad Character")]
        [RequiredArgument]
        public InArgument<char> PadCharacter { get; set; }

        [Input("Pad on the Left")]
        [RequiredArgument]
        [Default("true")]
        public InArgument<bool> PadOnLeft { get; set; }

        [Input("Final Length")]
        [RequiredArgument]
        [Default("30")]
        public InArgument<int> FinalLength { get; set; }

        [Output("Output String")]
        public OutArgument<string> OutputString { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputString = InputString.Get(context);
            var padCharacter = PadCharacter.Get(context);
            var padOnLeft = PadOnLeft.Get(context);
            var finalLength = FinalLength.Get(context);

            // TODO: Validate padCharacter is only one character?
            
            var output = PadString(inputString, padCharacter, padOnLeft, finalLength);
            OutputString.Set(context, output);
        }

        private string PadString(string inputString, char padCharacter, bool padOnLeft, int finalLength)
        {
            return padOnLeft ?
                inputString.PadLeft(finalLength, padCharacter) :
                inputString.PadRight(finalLength, padCharacter);
        }
    }
}
