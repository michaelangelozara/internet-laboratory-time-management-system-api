using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Roles;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class RoleClaimRepository(AppDbContext context)
    : IRoleClaimRepository
    {
        public async Task AddAsync(RoleClaim roleClaim)
            => await context.RoleClaims.AddAsync(roleClaim);

        public async Task<HashSet<string>> FindClaimsByUserId(Guid userId)
        {
            return await context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .SelectMany(r => r.RoleClaims)
                .Select(rc => rc.Value)
                .Distinct()
                .ToHashSetAsync();
        }
    }
}
