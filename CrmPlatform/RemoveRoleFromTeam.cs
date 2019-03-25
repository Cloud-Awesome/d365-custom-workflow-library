using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CrmPlatform
{
    public class RemoveRoleFromTeam: CodeActivity
    {
        [Input("Team")]
        [ReferenceTarget("team")]
        [RequiredArgument]
        public InArgument<EntityReference> TeamInArgument { get; set; }

        [Input("Security role to remove")]
        [ReferenceTarget("role")]
        [RequiredArgument]
        public InArgument<EntityReference> RoleToRemoveInArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext executionContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(executionContext.UserId);

            var team = TeamInArgument.Get(context);
            var role = RoleToRemoveInArgument.Get(context);

            RemoveRole(team, role, service);
        }

        private void RemoveRole(EntityReference team, EntityReference role, IOrganizationService service)
        {
            service.Disassociate("team", team.Id,
                new Relationship("teamroles_association"),
                new EntityReferenceCollection()
                {
                    new EntityReference(role.LogicalName, role.Id)
                });
        }
    }
}