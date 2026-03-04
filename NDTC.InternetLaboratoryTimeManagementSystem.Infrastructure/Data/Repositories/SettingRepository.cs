using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.SyncRequests;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class SettingRepository(AppDbContext context)
        : ISyncRequestRepository
    {
        public async Task<SyncRequest> FindByNameAsync(string name)
        {
            return await context.SyncRequests
                .SingleAsync(sr => sr.Name == name);
        }
    }
}
