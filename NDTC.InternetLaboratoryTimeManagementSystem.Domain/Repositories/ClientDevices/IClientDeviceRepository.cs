using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.ClientDevices;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices
{
    public interface IClientDeviceRepository 
        : IBaseRepository<ClientDevice>, IPagedRepository<ClientDeviceResponseDTO>
    {
        Task<ClientDevice?> FindByNameAsNoTrackingAsync(string deviceName);

        Task<ClientDevice?> FindByConnectionIdAsync(string connectionId);
    }
}
