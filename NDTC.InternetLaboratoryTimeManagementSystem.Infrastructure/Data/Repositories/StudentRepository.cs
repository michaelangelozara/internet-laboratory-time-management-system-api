using EFCore.BulkExtensions;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class StudentRepository(AppDbContext context)
        : IStudentRepository
    {
        public async Task BulkMergeAsync(IEnumerable<User> users)
        {
            await context.BulkInsertOrUpdateAsync(users, options =>
            {
                options.UpdateByProperties = ["school_id"];
            });
        }

        public async Task<PagedResult<BasicStudentResponseDTO>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await context.Students
                .Select(s => new BasicStudentResponseDTO(
                    s.Id,
                    s.User.SchoolId,
                    s.FirstName,
                    s.MiddleName,
                    s.LastName,
                    s.CourseCode,
                    s.SchoolYear,
                    s.EnrollmentStatus))
                .ToPagedResultAsync(pageNumber, pageSize);
        }
    }
}
