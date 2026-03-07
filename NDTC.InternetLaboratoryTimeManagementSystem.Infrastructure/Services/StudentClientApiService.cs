using Microsoft.Extensions.Logging;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.SyncRequests;


namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class StudentClientApiService(
        ISyncLockService syncLockService,
        ISyncRequestService syncRequestService,
        ILogger<StudentClientApiService> logger,
        ISyncEnrolledStudentHubService syncEnrolledStudentService)
        : IStudentClientApiService
    {
        public async Task TryToSyncEnrolledStudents()
        {
            logger.LogInformation("Trying to sync");

            var studentSyncRequest = await syncRequestService.GetAsync(SyncNames.StudentSync);
            if (studentSyncRequest.Status != SyncRequestStatus.Pending)
                return;

            var instanceId = Environment.MachineName;

            var acquire = await syncLockService.TryAcquireAsync(SyncNames.StudentSync, instanceId);
            if (!acquire)
                return;

            logger.LogInformation("Syncing enrolled students");

            // call the third party api

            try
            {
                await syncRequestService.MarkAsRunningAsync(SyncNames.StudentSync);

                await Sync();

                await syncRequestService.MarkAsCompletedAsync(SyncNames.StudentSync);
            }
            catch (Exception)
            {
                logger.LogError("Something went wrong while syncing students from third api");
                throw;
            }
            finally
            {
                await syncLockService.ReleaseAsync(SyncNames.StudentSync, instanceId);
            }
        }

        private static List<string> GetStudents(int numberOfStudents)
        {
            List<string> a = [];
            for(int i = 0; i < numberOfStudents; i++)
            {
                a.Add($"{i}");
            }

            return a;
        }

        private async Task Sync()
        {
            // perform syncing

            DateTime lastUpdate = DateTime.UtcNow;
            TimeSpan interval = TimeSpan.FromMicroseconds(10);

            var students = GetStudents(11620);
            int totalStudents = students.Count;

            int batchSize = 30;

            for (int i = 0; i < students.Count; i += batchSize)
            {
                var batch = students.Skip(i).Take(batchSize).ToList();

                DateTime now = DateTime.UtcNow;
                if ((now - lastUpdate) >= interval)
                {
                    float processedPercentage = (float) i / totalStudents * 100;
                    await syncEnrolledStudentService.PublishEnrolledStudentSyncingProgress($"{processedPercentage:F2}");

                    lastUpdate = now;
                }
            }

            await syncEnrolledStudentService.PublishEnrolledStudentSyncingProgress("100");

        }
    }
}
