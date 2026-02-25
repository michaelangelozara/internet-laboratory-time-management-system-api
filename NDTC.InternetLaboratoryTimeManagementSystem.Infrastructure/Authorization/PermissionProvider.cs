using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Authorization
{
    internal class PermissionProvider(IRoleManager roleManager)
    {
        public async Task<HashSet<string>> GetForUserIdAsync(Guid userId)
        {
            var permissionsSet = await roleManager.GetClaimsStrAsync(userId);
            return permissionsSet;
        }
    }
}
