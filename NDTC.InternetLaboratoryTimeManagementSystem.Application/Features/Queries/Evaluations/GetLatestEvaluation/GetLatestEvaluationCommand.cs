using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Evaluations.GetLatestEvaluation
{
    public sealed record GetLatestEvaluationCommand() : IRequest<Result<EvaluationResponseDTO>>;
}
