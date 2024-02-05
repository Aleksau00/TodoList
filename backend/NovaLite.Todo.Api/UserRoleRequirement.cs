using Microsoft.AspNetCore.Authorization;

namespace NovaLite.Todo.Api
{
    public class UserRoleRequirement : IAuthorizationRequirement
    {
        public string RequiredRole { get; }

        public UserRoleRequirement(string requiredRole)
        {
            RequiredRole = requiredRole;
        }
    }
}
