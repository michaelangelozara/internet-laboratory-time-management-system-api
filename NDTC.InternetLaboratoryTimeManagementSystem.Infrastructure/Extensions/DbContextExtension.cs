using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions
{
    internal static class DbContextExtension
    {
        public static void ApplyAuditing(this AppDbContext context)
        {
            var utcNow = DateTime.UtcNow;

            foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(IAuditable.CreatedAt))
                         .CurrentValue = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameof(IAuditable.LastModifiedAt))
                         .CurrentValue = utcNow;
                }
            }
        }
    }
}
