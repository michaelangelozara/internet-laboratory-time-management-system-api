namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations
{
    public interface IAnswerEvaluationRepository
    {
        Task<bool> DoesExistByEvaluationIdAndUserIdAsync(Guid evaluationId, Guid userId);
    }
}
