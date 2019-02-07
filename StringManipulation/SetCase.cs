using System.Activities;
using System.Globalization;
using Microsoft.Xrm.Sdk.Workflow;

namespace StringManipulation
{
    public class SetCase: CodeActivity
    {
        public string GetSetCase(string inputString, string caseOption)
        {
            switch (caseOption.ToLower())
            {
                case "lower":
                    return inputString.ToLower();
                case "upper":
                    return inputString.ToUpper();
                default: //Proper
                    var cultureInfo = CultureInfo.CurrentCulture;
                    var textInfo = cultureInfo.TextInfo;
                    return textInfo.ToTitleCase(inputString);
            }
        }

        [Input("Input String")]
        public InArgument<string> StringInArgument { get; set; }

        [Input("Case: 'UPPER', 'lower' or 'Proper'")]
        public InArgument<string> CaseOptionInArgument { get; set; }

        [Output("Return String")]
        public OutArgument<string> StringOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var inputString = StringInArgument.Get(context);
            var caseOption = CaseOptionInArgument.Get(context);

            var returnString = GetSetCase(inputString, caseOption);

            StringOutArgument.Set(context, returnString);
        }
    }
}