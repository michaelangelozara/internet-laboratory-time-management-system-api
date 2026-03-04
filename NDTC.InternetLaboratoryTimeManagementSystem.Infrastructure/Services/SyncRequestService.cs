using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
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
    }
}
