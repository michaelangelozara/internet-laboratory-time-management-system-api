using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class StudentRepository(AppDbContext context)
        : IStudentRepository
    {
        public async Task BulkMergeAsync(IEnumerable<Student> students)
        {
            await context.BulkInsertOrUpdateAsync(students, options =>
            {
                options.UpdateByProperties = [nameof(Student.SchoolId)];
            });
        }

        public async Task<PagedResult<BasicStudentResponseDTO>> GetPagedAsync(int pageNumber, int pageSize, string? query)
        {
            var students = context.Students
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query))
            {
                string q = query.Trim();
                students = students.Where(s => 
                    EF.Functions.ILike(s.FirstName, $"{q}%") ||
                    EF.Functions.ILike(s.LastName, $"{q}%"));
            }

            return await students
                .Select(s => new BasicStudentResponseDTO(
                    s.Id,
                    s.SchoolId,
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
