using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class ClientDeviceService(
        IClientDeviceRepository clientDeviceRepository,
        IUnitOfWork unitOfWork)
        : IClientDeviceService
    {
        public async Task RegisterDevice(string name, string connectionId)
        {
            var clientDevice = ClientDevice.Create(name, connectionId);
            await clientDeviceRepository.AddAsync(clientDevice);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task UnregisterDevice(string connectionId)
        {
            var clientDevice = await clientDeviceRepository.FindByConnectionIdAsync(connectionId);
            
            if (clientDevice is null)
                return;

            clientDeviceRepository.Remove(clientDevice);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
