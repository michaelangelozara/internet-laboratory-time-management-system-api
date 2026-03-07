namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services
{
    public interface IClientDeviceService
    {
        Task RegisterDevice(string name, string connectionId);

        Task UnregisterDevice(string connectionId);
    }
}
