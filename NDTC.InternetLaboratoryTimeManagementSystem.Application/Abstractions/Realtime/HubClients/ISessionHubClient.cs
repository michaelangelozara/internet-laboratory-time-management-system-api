namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients
{
    public interface ISessionHubClient
    {
        Task NewSession(Guid userId, string schoolId, TimeSpan duration);

        Task LoggedOutSession(Guid userId);

        Task UpdatedSession(TimeSpan duration);

        Task Terminate();

        Task Restart();
    }
}
