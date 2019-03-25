using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CrmPlatform
{
    public class UserHasSecurityRole: CodeActivity
    {
        [Input("User")]
        [RequiredArgument]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> UserInArgument { get; set; }

        [Input("Security Role")]
        [RequiredArgument]
        [ReferenceTarget("role")]
        public InArgument<EntityReference> RoleInArgument { get; set; }

        [Output("Has Security Role")]
        public OutArgument<bool> HasSecurityRoleOutArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            throw new System.NotImplementedException();
            //TODO - Sample Code: https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/sample-determine-user-role
        }
    }
}