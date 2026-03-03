using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Settings;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class SettingRepository(AppDbContext context)
        : ISettingRepository
    {
        public async Task<Setting> Find()
        {
            return await context.Settings
                .FirstOrDefaultAsync() ?? 
                throw new ApplicationException("Setting is null. Seed it first.");
        }
    }
}
