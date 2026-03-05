using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface ISyncRequestService
    {
        Task<SyncRequest> GetAsync(string name);

        Task MarkAsRunningAsync(string name);

        Task MarkAsCompletedAsync(string name);

        Task StopSyncingAsync();
    }
}
