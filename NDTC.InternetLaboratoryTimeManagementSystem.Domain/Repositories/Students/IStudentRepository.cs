using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students
{
    public interface IStudentRepository : IPagedRepository<BasicStudentResponseDTO>
    {
        Task BulkMergeAsync(IEnumerable<User> users);
    }
}
