using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.SyncRequests
{
    public interface ISyncRequestRepository
    {
        Task<SyncRequest> FindByNameAsync(string name);
    }
}
