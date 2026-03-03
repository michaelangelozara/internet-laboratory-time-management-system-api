using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Evaluations.GetPagedEvaluations
{
    public sealed record GetPagedEvaluationQuery(int PageNumber, int PageSize)
        : IRequest<Result<PagedResult<EvaluationResponseDTO>>>;
}
