namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients
{
    public interface ISessionHubClient
    {
        Task NewSession(string schoolId, TimeSpan duration);

        Task LoggedOutSession(Guid userId);

        Task UpdatedSession(TimeSpan duration);

        Task Terminate();
    }
}
