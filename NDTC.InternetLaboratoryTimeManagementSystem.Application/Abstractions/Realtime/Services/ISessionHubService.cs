namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services
{
    public interface ISessionHubService
    {
        Task PublishNewSessionOf(Guid userId, string schoolId, TimeSpan availableDuration);

        Task PublishLoggedOutSessionOf();

        Task PublishUpdatedSession(Guid userId, TimeSpan duration);

        Task PublishTerminationTo(Guid userId);

        Task PublishTerminatedUserId(Guid userId);
    }
}
