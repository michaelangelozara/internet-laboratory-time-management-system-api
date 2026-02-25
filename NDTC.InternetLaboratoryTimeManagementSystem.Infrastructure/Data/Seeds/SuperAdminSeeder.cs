using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Seeds
{
    public static class SuperAdminSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context,
            CancellationToken cancellationToken)
        {
            var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.SuperAdmin, cancellationToken)
                ?? throw new InvalidDataException("SuperAdmin cannot be seeded. Role not found.");

            bool isSuperAdminCreated = await context.UserRoles.AnyAsync(ur => ur.RoleId == role.Id, cancellationToken);
            if (isSuperAdminCreated)
                return;

            var superAdmin = User.Create("superadmin", "superadmin");
            await context.Users.AddAsync(superAdmin, cancellationToken);

            var userRole = UserRole.CreateUserRole(superAdmin, role);
            await context.UserRoles.AddAsync(userRole, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
