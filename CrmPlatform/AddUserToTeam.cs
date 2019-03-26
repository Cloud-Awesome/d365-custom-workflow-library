using System.Activities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CrmPlatform
{
    public class AddUserToTeam: CodeActivity
    {
        [Input("User to add")]
        [ReferenceTarget("systemuser")]
        [RequiredArgument]
        public InArgument<EntityReference> UserToAddInArgument { get; set; }

        [Input("Team to join")]
        [ReferenceTarget("team")]
        [RequiredArgument]
        public InArgument<EntityReference> TeamToJoinInArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var executionContext = context.GetExtension<IWorkflowContext>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(executionContext.UserId);

            var userToAdd = UserToAddInArgument.Get(context);
            var teamToJoin = TeamToJoinInArgument.Get(context);

            var result = AddUser(userToAdd, teamToJoin, service);
        }

        private ParameterCollection AddUser(EntityReference user, EntityReference team, IOrganizationService service)
        {
            var request = new AddMembersTeamRequest
            {
                MemberIds = new[] { user.Id },
                TeamId = team.Id
            };
            
            var response = (AddMembersTeamResponse)service.Execute(request);
            return response.Results;
        }

    }
}
