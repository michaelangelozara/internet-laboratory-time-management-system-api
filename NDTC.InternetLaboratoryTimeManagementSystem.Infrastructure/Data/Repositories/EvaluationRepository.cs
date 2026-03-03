using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Enums.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class EvaluationRepository(AppDbContext context)
        : BaseRepository<Evaluation>(context), IEvaluationRepository
    {
        public async Task DeactivateAllEvaluations()
        {
            await context.Evaluations
                .Where(e => e.Active)
                .ExecuteUpdateAsync(setter =>
                    setter
                        .SetProperty(e => e.Active, false));
        }

        public async Task<Evaluation?> FindByIdAsync(Guid id)
        {
            return await context.Evaluations
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EvaluationResponseDTO?> FindEvaluationResponseDTOByIdAsync()
        {
            var result = await context.Evaluations
                .AsNoTracking()
                .Where(e => e.Active)
                .Select(e => new
                {
                    e.Id,
                    e.Question,
                    e.CreatedAt,
                    e.LastModifiedAt,
                    TotalAnswers = e.AnswerEvaluations.Count(),
                    LikedCount = e.AnswerEvaluations.Count(ae => ae.Type == EvaluationType.Liked),
                    DislikedCount = e.AnswerEvaluations.Count(ae => ae.Type == EvaluationType.Disliked)
                })
                .FirstOrDefaultAsync();

            if (result == null)
                return null;

            float likedPercentage = result.TotalAnswers > 0
                ? (float)result.LikedCount / result.TotalAnswers * 100
                : 0;
            float dislikedPercentage = result.TotalAnswers > 0
                ? (float)result.DislikedCount / result.TotalAnswers * 100
                : 0;

            return new EvaluationResponseDTO(
                result.Id,
                result.Question,
                likedPercentage,
                dislikedPercentage,
                result.TotalAnswers,
                result.CreatedAt,
                result.LastModifiedAt
            );
        }

        public async Task<PagedResult<EvaluationResponseDTO>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await context.Evaluations
                .AsNoTracking()
                .OrderByDescending(e => e.CreatedAt)
                .Select(e => new EvaluationResponseDTO(
                    e.Id,
                    e.Question,
                    (float)e.AnswerEvaluations.Count(ae => ae.Type == EvaluationType.Liked) / e.AnswerEvaluations.Count() * 100,
                    (float)e.AnswerEvaluations.Count(ae => ae.Type == EvaluationType.Disliked) / e.AnswerEvaluations.Count() * 100,
                    e.AnswerEvaluations.Count(),
                    e.CreatedAt,
                    e.LastModifiedAt))
                .ToPagedResultAsync(pageNumber, pageSize);
        }
    }
}
