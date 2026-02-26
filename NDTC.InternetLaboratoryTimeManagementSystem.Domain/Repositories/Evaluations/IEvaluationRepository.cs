using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations
{
    public interface IEvaluationRepository : IBaseRepository<Evaluation>
    {
        Task DeactivateAllEvaluations();
    }
}
