using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Evaluations.GetPagedEvaluations
{
    internal class GetPagedEvaluationQueryHandler(IEvaluationRepository evaluationRepository)
        : IRequestHandler<GetPagedEvaluationQuery, Result<PagedResult<EvaluationResponseDTO>>>
    {
        public async Task<Result<PagedResult<EvaluationResponseDTO>>> Handle(GetPagedEvaluationQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(await evaluationRepository.GetPagedAsync(request.PageNumber, request.PageSize));
        }
    }
}
