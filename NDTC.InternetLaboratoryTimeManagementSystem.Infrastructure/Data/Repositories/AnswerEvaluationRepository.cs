using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class AnswerEvaluationRepository(AppDbContext context)
        : IAnswerEvaluationRepository
    {
        public async Task<bool> DoesExistByEvaluationIdAndUserIdAsync(Guid evaluationId, Guid userId)
        {
            return await context.AnswerEvaluations
                .AnyAsync(ae => ae.EvaluationId == evaluationId && ae.UserId == userId);
        }
    }
}
