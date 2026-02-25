using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Roles;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class RoleRepository(AppDbContext context)
    : IRoleRepository
    {
        public async Task AddAsync(Role role)
            => await context.Roles.AddAsync(role);

        public async Task<Role?> FindByNameAsNoTrackingAsync(string roleName)
            => await context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name == roleName);

        public async Task<Role?> FindByNameAsync(string roleName)
            => await context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleName);
    }
}
