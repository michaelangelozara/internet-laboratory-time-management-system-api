using Microsoft.Extensions.Logging;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Constants;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.SyncRequests;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.ThirdPartyApi;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.ThirdPartyApi.DTOs;


namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class StudentService(
        ISyncLockService syncLockService,
        ISyncRequestService syncRequestService,
        ILogger<StudentService> logger,
        ISyncEnrolledStudentHubService syncEnrolledStudentService,
        StudentServiceClientApi studentServiceClientApi,
        IStudentRepository studentRepository,
        IUserRepository userRepository,
        IAccountRepository accountRepository
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

            try
            {
                await syncRequestService.MarkAsRunningAsync(SyncNames.StudentSync);

                await Sync();
            }
            catch (Exception)
            {
                logger.LogError("Something went wrong while syncing students from third api");
                throw;
            }
            finally
            {
                await syncRequestService.MarkAsCompletedAsync(SyncNames.StudentSync);

                await syncLockService.ReleaseAsync(SyncNames.StudentSync, instanceId);
            }
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

            var filteredStudents = GetFilteredCurrentEnrolledStudents(enrolledStudentsResult);

            int totalStudents = filteredStudents.Total;

            DateTime lastUpdate = DateTime.UtcNow;
            TimeSpan interval = TimeSpan.FromSeconds(1);

            int batchSize = 20;

            for (int i = 0; i < totalStudents; i += batchSize)
            {
                var users = GetBatchedUsers(filteredStudents.Data, i, batchSize);
                var students = GetBatchedStudents(users);
                var accounts = GetBatchedAccounts(users);

                // perform merging
                await userRepository.BulkMergeAsync(users);
                await studentRepository.BulkMergeAsync(students);
                await accountRepository.BulkMergeAsync(accounts);

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

        private static List<User> GetBatchedUsers(IEnumerable<DetailedStudentResponseDTO> enrolledStudents, int i, int batchSize)
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
                    es.EnrollmentStatus,
                    user.SchoolId,
                    user.Id);

                user.SetStudent(student);

                return user;
            })
                .Skip(i)
                .Take(batchSize)];
        }

        private static List<Account> GetBatchedAccounts(List<User> users)
        {
            // extract the accounts from users
            return [.. users.Select(u => u.Account!)];
        }

        private static List<Student> GetBatchedStudents(List<User> users)
        {
            // extract the students from users
            return [.. users.Select(u => u.Student!)];
        }

        private static string GetCurrentSchoolYear()
        {
            const int SchoolYearStartMonth = 7;
            var now = DateTime.UtcNow;

            int startYear = now.Month >= SchoolYearStartMonth ? now.Year : now.Year - 1;
            int endYear = startYear + 1;

            return $"{startYear}-{endYear}";
        }

        private static string GetCurrentSemester()
        {
            const int SchoolYearStartMonth = 7;
            var now = DateTime.UtcNow;

            return now.Month >= SchoolYearStartMonth ? "1ST" : "2ND";
        }

        private static StudentClientApiResponseDTO GetFilteredCurrentEnrolledStudents(StudentClientApiResponseDTO studentClientApiResponseDTO)
        {
            var currentShoolYear = GetCurrentSchoolYear();
            var currentSemester = GetCurrentSemester();

            var currentEnrolledStudents = studentClientApiResponseDTO.Data
                .Where(dsrDTO => dsrDTO.SchoolYear == currentShoolYear && dsrDTO.Semester == currentSemester)
                .ToList();

            int total = currentEnrolledStudents.Count;

            return new StudentClientApiResponseDTO
            {
                Success = studentClientApiResponseDTO.Success,
                Data = currentEnrolledStudents,
                SchoolYear = studentClientApiResponseDTO.SchoolYear,
                Total = total
            };
        }
    }
}
