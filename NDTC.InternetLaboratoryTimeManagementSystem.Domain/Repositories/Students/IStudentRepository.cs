using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Students;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Students
{
    public interface IStudentRepository : IPagedRepository<BasicStudentResponseDTO, string?>
    {
        Task BulkMergeAsync(IEnumerable<Student> students);
    }
}
