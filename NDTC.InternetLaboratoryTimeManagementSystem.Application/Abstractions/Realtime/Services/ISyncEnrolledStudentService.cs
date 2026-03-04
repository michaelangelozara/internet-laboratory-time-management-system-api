namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services
{
    public interface ISyncEnrolledStudentService
    {
        Task PublishEnrolledStudentSyncingProgress(string processedPercentage);
    }
}
