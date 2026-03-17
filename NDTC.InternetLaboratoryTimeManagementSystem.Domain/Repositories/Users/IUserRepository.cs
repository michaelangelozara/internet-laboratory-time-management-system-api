using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task BulkMergeAsync(IEnumerable<User> users);
    }
}
