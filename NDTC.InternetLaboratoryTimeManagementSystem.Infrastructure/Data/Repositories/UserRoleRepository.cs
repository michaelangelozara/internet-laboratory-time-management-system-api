using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Database.Repositories
{
    internal class UserRoleRepository(AppDbContext context)
    : IUserRoleRepository
    {
        public async Task AddAsync(UserRole userRole)
            => await context.UserRoles.AddAsync(userRole);
    }
}
