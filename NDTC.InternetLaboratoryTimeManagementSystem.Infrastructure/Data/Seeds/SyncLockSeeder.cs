using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Seeds
{
    public static class SyncLockSeeder
    {
        public static async Task SeedAsync(
           AppDbContext context,
           CancellationToken cancellationToken)
        {
            await SeedStudentSync(context, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        private static async Task SeedStudentSync(
           AppDbContext context,
           CancellationToken cancellationToken)
        {
            var exists = await context.SyncLocks.AnyAsync(sl => sl.Name == SyncNames.StudentSync, cancellationToken);
            if (exists)
                return;

            var syncLock = SyncLock.Create(SyncNames.StudentSync);
            await context.SyncLocks.AddAsync(syncLock, cancellationToken);
        }
    }
}
