using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class SyncLockService(AppDbContext context)
        : ISyncLockService
    {
        public async Task ReleaseAsync(string name, string instanceId)
        {
            await context.SyncLocks
                .Where(sl => sl.Name == name && sl.LockedBy == instanceId)
                .ExecuteUpdateAsync(setter =>
                    setter
                        .SetProperty(sl => sl.IsRunning, false)
                        .SetProperty(sl => sl.LockedBy, string.Empty));
        }

        public async Task<bool> TryAcquireAsync(string name, string instanceId)
        {
            int affectedRow = await context.SyncLocks
                .Where(sl => sl.Name == name && !sl.IsRunning)
                .ExecuteUpdateAsync(setter => 
                    setter
                        .SetProperty(sl => sl.IsRunning, true)
                        .SetProperty(sl => sl.LockedAt, DateTime.UtcNow)
                        .SetProperty(sl => sl.LockedBy, instanceId));

            return affectedRow == 1;
        }
    }
}
