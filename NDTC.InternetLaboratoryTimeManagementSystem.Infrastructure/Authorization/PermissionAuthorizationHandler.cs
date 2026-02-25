using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Authentication;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Authorization
{
    internal class PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (context.User?.Identity?.IsAuthenticated != true)
            {
                return; // reject unauthenticated users
            }

            using IServiceScope scope = serviceScopeFactory.CreateScope();

            PermissionProvider permissionProvider = scope.ServiceProvider.GetRequiredService<PermissionProvider>();

            Guid userId = context.User.GetUserId();

            HashSet<string> permissions = await permissionProvider.GetForUserIdAsync(userId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
