using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CrmPlatform
{
    public class AddRoleToTeam: CodeActivity
    {
        [Input("Team")]
        [ReferenceTarget("team")]
        [RequiredArgument]
        public InArgument<EntityReference> TeamInArgument { get; set; }

        [Input("Security role to add")]
        [ReferenceTarget("role")]
        [RequiredArgument]
        public InArgument<EntityReference> RoleToAddInArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var executionContext = context.GetExtension<IWorkflowContext>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(executionContext.UserId);

            var team = TeamInArgument.Get(context);
            var role = RoleToAddInArgument.Get(context);

            AddRole(team, role, service);
        }

        private void AddRole(EntityReference team, EntityReference role, IOrganizationService service)
        {
            service.Associate("team", team.Id,
                new Relationship("teamroles_association"), new EntityReferenceCollection()
                {
                    new EntityReference(role.LogicalName, role.Id)
                });
        }
    }
}