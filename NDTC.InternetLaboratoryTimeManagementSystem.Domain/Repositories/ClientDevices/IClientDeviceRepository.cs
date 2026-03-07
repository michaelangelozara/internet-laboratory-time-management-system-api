using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices
{
    public interface IClientDeviceRepository
    {
        Task<ClientDevice?> FindByName(string deviceName);
    }
}
