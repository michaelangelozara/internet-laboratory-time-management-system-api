using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students
{
    public interface IStudentRepository
    {
        Task BulkMergeAsync(IEnumerable<User> users);
    }
}
