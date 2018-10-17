using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CustomWorkflowLibrary.DataConversion
{
    public class ConvertToDateTime: CodeActivity
    {
        [Input("String input")]
        [RequiredArgument]
        public InArgument<string> StringInArgument { get; set; }

        [Output("DateTime Output")]
        public OutArgument<DateTime> DateTimeOutArgument { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            var t = context.GetExtension<ITracingService>();

            var stringInput = StringInArgument.Get(context);

            try
            {
                var returnValue = DoConversion(stringInput);
                DateTimeOutArgument.Set(context, returnValue);
            }
            catch (FormatException)
            {
                t.Trace($"{DateTime.Now}: Could not convert input value - a formatting exception was thrown.");
                throw;
            }
            catch (Exception)
            {
                t.Trace($"{DateTime.Now}: Could not convert input value - a generic exception was thrown.");
                throw;
            }
        }

        public DateTime DoConversion(string inputString)
        {
            return Convert.ToDateTime(inputString);
        }
    }
}
