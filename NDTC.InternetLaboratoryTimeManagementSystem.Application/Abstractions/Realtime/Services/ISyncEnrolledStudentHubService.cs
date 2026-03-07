namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services
{
    public interface ISyncEnrolledStudentHubService
    {
        Task PublishEnrolledStudentSyncingProgress(string processedPercentage);
    }
}
