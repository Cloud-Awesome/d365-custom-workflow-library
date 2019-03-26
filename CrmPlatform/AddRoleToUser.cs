using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace CrmPlatform
{
    public class AddRoleToUser: CodeActivity
    {

        [Input("User")]
        [ReferenceTarget("systemuser")]
        [RequiredArgument]
        public InArgument<EntityReference> UserInArgument { get; set; }

        [Input("Security role to add")]
        [ReferenceTarget("role")]
        [RequiredArgument]
        public InArgument<EntityReference> RoleToAddInArgument { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var executionContext = context.GetExtension<IWorkflowContext>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(executionContext.UserId);

            var user = UserInArgument.Get(context);
            var role = RoleToAddInArgument.Get(context);

            AddRole(user, role, service);
        }

        private void AddRole(EntityReference user, EntityReference role, IOrganizationService service)
        {
            service.Associate("systemuser", user.Id,
                new Relationship("systemuserroles_association"), new EntityReferenceCollection()
                {
                    new EntityReference(role.LogicalName, role.Id)
                });
        }
    }
}
