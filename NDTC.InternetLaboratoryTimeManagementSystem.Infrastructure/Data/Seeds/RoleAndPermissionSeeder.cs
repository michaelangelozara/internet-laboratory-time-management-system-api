using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Seeds
{
    public static class RoleAndPermissionSeeder
    {
        public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken)
        {
            await SuperAdminRole(context, cancellationToken);

            await AdminRole(context, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        #region SuperAdmin role configuration
        private static async Task SuperAdminRole(AppDbContext context, CancellationToken cancellationToken)
        {
            var superAdminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.SuperAdmin, cancellationToken);
            if (superAdminRole is null)
            {
                await context.Roles.AddAsync(superAdminRole = Role.Create(Roles.SuperAdmin), cancellationToken);

                superAdminRole.AddRoleClaims(
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Create),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Delete),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Read),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Update),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Account.UpdateRFID),

                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Evaluation.Create),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Evaluation.Delete),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Evaluation.Update)
                    );
            }
        }
        #endregion

        #region Admin role configuration
        private static async Task AdminRole(AppDbContext context, CancellationToken cancellationToken)
        {
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == Roles.Admin, cancellationToken);
            if (adminRole is null)
            {
                await context.Roles.AddAsync(adminRole = Role.Create(Roles.Admin), cancellationToken);

                adminRole.AddRoleClaims(
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Create),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Delete),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Read),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.User.Update),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Account.UpdateRFID),

                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Evaluation.Create),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Evaluation.Delete),
                    RoleClaim.Create(CustomClaimType.Permission, Permissions.Evaluation.Update));
            }
        }
        #endregion
    }
}
