namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface ISettingService
    {
        Task<bool> IsSyncing();
    }
}
