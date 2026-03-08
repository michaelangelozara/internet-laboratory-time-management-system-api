namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services
{
    public interface IClientDeviceService
    {
        Task RegisterDevice(string name, string connectionId);

        Task UnregisterDevice(string connectionId);

        Task RemoveAllRegisteredDevices();
    }
}
