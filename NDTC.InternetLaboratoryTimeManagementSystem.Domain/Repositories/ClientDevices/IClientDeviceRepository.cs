using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.ClientDevices;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices
{
    public interface IClientDeviceRepository : IPagedRepository<ClientDeviceResponseDTO>
    {
        Task<ClientDevice?> FindByName(string deviceName);
    }
}
