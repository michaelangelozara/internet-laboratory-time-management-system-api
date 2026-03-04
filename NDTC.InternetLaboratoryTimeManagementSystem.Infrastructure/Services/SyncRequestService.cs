using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.SyncRequests;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class SyncRequestService(AppDbContext context)
        : ISyncRequestService
    {
        public async Task<SyncRequest> GetAsync(string name)
        {
            return await context.SyncRequests
                .SingleAsync(sr => sr.Name == name);
        }

        public async Task MarkAsCompletedAsync(string name)
        {
            await UpdateStatusAsync(name, SyncRequestStatus.Completed);
        }

        public async Task MarkAsRunningAsync(string name)
        {
            await UpdateStatusAsync(name, SyncRequestStatus.Running);
        }

        private async Task UpdateStatusAsync(string name, SyncRequestStatus status)
        {
            await context.SyncRequests
                .Where(sr => sr.Name == name)
                .ExecuteUpdateAsync(setter =>
                    setter
                        .SetProperty(sr => sr.Status, status));
        }
    }
}
