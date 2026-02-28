using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations
{
    public interface IEvaluationRepository : IBaseRepository<Evaluation>
    {
        Task DeactivateAllEvaluations();

        Task<Evaluation?> FindByIdAsync(Guid id);

        Task<EvaluationResponseDTO?> FindEvaluationResponseDTOByIdAsync();
    }
}
