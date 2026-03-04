namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.HubClients
{
    public interface ISyncEnrolledStudentHubClient
    {
        Task SyncingProgress(string processedPercentage);
    }
}
