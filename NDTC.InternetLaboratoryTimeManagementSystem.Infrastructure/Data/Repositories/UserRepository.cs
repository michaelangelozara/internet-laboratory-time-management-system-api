using EFCore.BulkExtensions;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class UserRepository(AppDbContext context)
        : BaseRepository<User>(context), IUserRepository
    {
        public async Task BulkMergeAsync(IEnumerable<User> users)
        {
            await context.BulkInsertOrUpdateAsync(users, options =>
            {
                options.UpdateByProperties = [nameof(User.SchoolId)];
            });
        }
    }
}
