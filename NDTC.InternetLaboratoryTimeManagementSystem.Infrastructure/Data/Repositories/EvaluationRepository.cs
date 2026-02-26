using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;

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
    }
}
