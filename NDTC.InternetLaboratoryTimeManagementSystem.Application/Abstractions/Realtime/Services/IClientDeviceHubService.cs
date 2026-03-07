namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services
{
    public interface IClientDeviceHubService
    {
        Task PublishRestartSignalTo(string connectionId);
    }
}
