using Microsoft.Extensions.Logging;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.SyncRequests;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.ThirdPartyApi;


namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class StudentService(
        ISyncLockService syncLockService,
        ISyncRequestService syncRequestService,
        ILogger<StudentService> logger,
        ISyncEnrolledStudentHubService syncEnrolledStudentService,
        StudentServiceClientApi studentServiceClientApi,
        IStudentRepository studentRepository
        )
        : IStudentService
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

        private static List<User> GetUsers(IEnumerable<StudentResponseDTO> enrolledStudents, int i, int batchSize)
        {
            // convert to users
            return [.. enrolledStudents.Select(es =>
            {
                var user = User.Create(es.Student_UID, es.RFIDNumber);

                var student = Student.Create(
                    es.FirstName,
                    es.MiddleName,
                    es.LastName,
                    es.NameSuffix,
                    es.BirthDate,
                    es.Gender,
                    es.ContactNumber,
                    es.Enrollment_UID,
                    es.CourseCode,
                    es.SchoolYear,
                    es.Semester,
                    es.EnrollmentStatus);

                user.SetStudent(student);

                return user;
            })
                .Skip(i)
                .Take(batchSize)];
        }

        private async Task Sync()
        {
            // perform syncing

            var enrolledStudentsResult = await studentServiceClientApi.GetEnrolledStudentsAsync();

            if (enrolledStudentsResult is null)
            {
                logger.LogError("Enrolled student is null.");
                return;
            }

            int totalStudents = enrolledStudentsResult.Total;

            DateTime lastUpdate = DateTime.UtcNow;
            TimeSpan interval = TimeSpan.FromSeconds(1);

            int batchSize = 25;

            for (int i = 0; i < totalStudents; i += batchSize)
            {
                var users = GetUsers(enrolledStudentsResult.Data, i, batchSize);

                // perform merging
                await studentRepository.BulkMergeAsync(users);

                DateTime now = DateTime.UtcNow;
                if ((now - lastUpdate) >= interval)
                {
                    float processedPercentage = (float)i / totalStudents * 100;
                    await syncEnrolledStudentService.PublishEnrolledStudentSyncingProgress($"{processedPercentage:F2}");

                    lastUpdate = now;
                }
            }

            await syncEnrolledStudentService.PublishEnrolledStudentSyncingProgress("100");

        }
    }
}
