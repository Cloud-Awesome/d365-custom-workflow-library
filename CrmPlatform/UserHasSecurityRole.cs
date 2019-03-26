using System.Activities;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
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
            var executionContext = context.GetExtension<IWorkflowContext>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(executionContext.UserId);

            var user = UserInArgument.Get(context);
            var role = RoleInArgument.Get(context);

            var result = HasSecurityRole(service, user, role);
            HasSecurityRoleOutArgument.Set(context, result);
        }

        private bool HasSecurityRole(IOrganizationService service, EntityReference user, 
            EntityReference role)
        {
            const string systemUserRolesEntityName = "systemuserroles";
            const string roleEntityName = "role";
            const string roleIdAttributeName = "roleid";
            const string userIdAttributeName = "systemuserid";

            var returnValue = false;

            // Get all roles with this name to capture parent-child roles in different BUs
            var query = new QueryExpression()
            {
                EntityName = systemUserRolesEntityName,
                ColumnSet = new ColumnSet(true),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(userIdAttributeName, 
                            ConditionOperator.Equal, user.Id)
                    }
                }
            };

            // Reference Roles entity to get Name (so comparison isn't based on GUID)
            var roles = new LinkEntity(systemUserRolesEntityName, roleEntityName,
                roleIdAttributeName, roleIdAttributeName, JoinOperator.Inner)
            {
                Columns = new ColumnSet("name"),
                EntityAlias = "roles"
            };

            query.LinkEntities.Add(roles);

            // Retrieve matching roles.
            var matchEntities = service.RetrieveMultiple(query);

            foreach (var entity in matchEntities.Entities)
            {
                var roleName = (entity["roles.name"] as AliasedValue)?.Value.ToString();
                if (roleName == role.Name)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }
    }
}