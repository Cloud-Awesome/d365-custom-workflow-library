using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CrmPlatform
{
    public class RemoveRoleFromUser: CodeActivity
    {
        [Input("User")]
        [ReferenceTarget("systemuser")]
        [RequiredArgument]
        public InArgument<EntityReference> UserInArgument { get; set; }

        [Input("Security role to remove")]
        [ReferenceTarget("role")]
        [RequiredArgument]
        public InArgument<EntityReference> RoleToRemoveInArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            IWorkflowContext executionContext = context.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(executionContext.UserId);

            var user = UserInArgument.Get(context);
            var role = RoleToRemoveInArgument.Get(context);

            RemoveRole(user, role, service);
        }

        private void RemoveRole(EntityReference user, EntityReference role, IOrganizationService service)
        {
            service.Disassociate("systemuser", user.Id, 
                new Relationship("systemuserroles_association"),
                new EntityReferenceCollection()
                {
                    new EntityReference(role.LogicalName, role.Id)
                });
        }
    }
}