using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NovaLite.Todo.Api
{
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserRoleAuthorizationHandler : AuthorizationHandler<UserRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserRoleRequirement requirement)
        {
            // Retrieve user role from the header
            var userRoleHeader = context.Resource as DefaultHttpContext;
            var userRole = userRoleHeader?.Request.Headers["Role"].FirstOrDefault();

            if (userRole != null && userRole.Equals(requirement.RequiredRole, System.StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

}
