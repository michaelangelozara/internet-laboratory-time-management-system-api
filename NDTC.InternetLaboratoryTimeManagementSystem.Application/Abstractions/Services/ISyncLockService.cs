namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface ISyncLockService
    {
        Task<bool> TryAcquireAsync(string name, string instanceId);

        Task ReleaseAsync(string name, string instanceId);
    }
}
