using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface IRoleManager
    {
        Task<Role?> FindByNameAsync(string roleName);

        Task<Role> CreateAsync(Role role);

        Task AddClaimAsync(Role role, RoleClaim userClaim);

        Task AddToRoleAsync(User user, string role);

        IList<string> GetRoles(User user);

        IList<string> GetClaims(User user);

        Task<HashSet<string>> GetClaimsStrAsync(Guid userId);
    }
}
