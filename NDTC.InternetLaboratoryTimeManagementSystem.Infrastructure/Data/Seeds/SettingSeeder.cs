using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Seeds
{
    public static class SettingSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context,
            CancellationToken cancellationToken)
        {
            var exists = await context.Settings.AnyAsync(cancellationToken);
            if (exists)
                return;

            var setting = Setting.Create();
            await context.Settings.AddAsync(setting, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
