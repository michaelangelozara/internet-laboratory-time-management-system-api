using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Seeds
{
    public static class SyncRequestSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context,
            CancellationToken cancellationToken)
        {

            await SeedStudentSyncRequest(context, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        private static async Task SeedStudentSyncRequest(
            AppDbContext context,
            CancellationToken cancellationToken)
        {
            var exists = await context.SyncRequests.AnyAsync(sr => sr.Name == SyncNames.StudentSync, cancellationToken);
            if (exists)
                return;

            var syncRequest = SyncRequest.Create(SyncNames.StudentSync);
            await context.SyncRequests.AddAsync(syncRequest, cancellationToken);
        }
    }
}
