using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Roles;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class RoleManager(
        IRoleRepository roleRepository,
        IRoleClaimRepository roleClaimRepository,
        IUserRoleRepository userRoleRepository)
        : IRoleManager
    {
        public async Task AddClaimAsync(Role role, RoleClaim userClaim)
        {
            userClaim.SetRole(role);
            await roleClaimRepository.AddAsync(userClaim);
        }

        public async Task AddToRoleAsync(User user, string roleName)
        {
            var role = await roleRepository.FindByNameAsync(roleName)
                ?? throw new InvalidOperationException("Invalid type of role.");

            await userRoleRepository.AddAsync(UserRole.Create(user, role));
        }

        public async Task<Role> CreateAsync(Role role)
        {
            await roleRepository.AddAsync(role);
            return role;
        }

        public async Task<Role?> FindByNameAsync(string roleName)
        {
            return await roleRepository.FindByNameAsNoTrackingAsync(roleName);
        }

        public IList<string> GetClaims(User user)
        {
            if (!HasRole(user))
                return [];

            bool hasRoleClaim = user.UserRoles
                .Select(ur => ur.Role)
                .Select(r => r.RoleClaims)
                .Any();

            if (!hasRoleClaim)
                return [];

            return [.. user.UserRoles
            .Select(ur => ur.Role)
            .SelectMany(r => r!.RoleClaims)
            .Select(rc => rc.Value)];
        }

        public async Task<HashSet<string>> GetClaimsStrAsync(Guid userId)
        {
            var permissionsSet = await roleClaimRepository.FindClaimsByUserId(userId);
            return permissionsSet;
        }

        public IList<string> GetRoles(User user)
        {
            if (!HasRole(user))
                return [];

            return [.. user.UserRoles
            .Select(ur => ur.Role)
            .Select(r => r!.Name)];
        }

        private bool HasRole(User user) => user.UserRoles.Any();
    }
}
