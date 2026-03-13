using EFCore.BulkExtensions;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students;

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
    }
}
