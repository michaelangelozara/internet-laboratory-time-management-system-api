using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using Quartz;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.BackgroundJobs
{
    internal class SyncEnrolledStudentsBackgroundJob(IStudentClientApiService studentClientApiService)
        : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await studentClientApiService.TryToSyncEnrolledStudents();
        }
    }
}
