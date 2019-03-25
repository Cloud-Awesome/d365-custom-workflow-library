using System.Activities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CrmPlatform
{
    public class RemoveUserFromTeam: CodeActivity
    {
        [Input("User to remove")]
        [ReferenceTarget("systemuser")]
        [RequiredArgument]
        public InArgument<EntityReference> UserToRemoveInArgument { get; set; }

        [Input("Team to leave")]
        [ReferenceTarget("team")]
        [RequiredArgument]
        public InArgument<EntityReference> TeamToLeaveInArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext executionContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(executionContext.UserId);

            var userToAdd = UserToRemoveInArgument.Get(context);
            var teamToJoin = TeamToLeaveInArgument.Get(context);

            var result = RemoveUser(userToAdd, teamToJoin, service);
        }

        private ParameterCollection RemoveUser(EntityReference user, EntityReference team, IOrganizationService service)
        {
            var request = new RemoveMembersTeamRequest
            {
                MemberIds = new[] { user.Id },
                TeamId = team.Id
            };

            var response = (RemoveMembersTeamResponse)service.Execute(request);
            return response.Results;
        }
    }
}
