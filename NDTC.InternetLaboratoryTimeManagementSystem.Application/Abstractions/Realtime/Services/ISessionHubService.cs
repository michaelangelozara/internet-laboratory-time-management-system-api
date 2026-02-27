namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services
{
    public interface ISessionHubService
    {
        Task PublishNewSessionOf(string schoolId, TimeSpan availableDuration);

        Task PublishLoggedOutSessionOf();

        Task PublishUpdatedSession(Guid userId, TimeSpan duration);

        Task PublishTerminationTo(Guid userId);
    }
}
