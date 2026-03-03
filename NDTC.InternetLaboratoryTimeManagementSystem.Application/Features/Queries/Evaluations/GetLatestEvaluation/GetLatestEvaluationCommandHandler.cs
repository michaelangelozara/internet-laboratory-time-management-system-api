using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Evaluations.GetLatestEvaluation
{
    internal class GetLatestEvaluationCommandHandler(
        IEvaluationRepository evaluationRepository)
        : IRequestHandler<GetLatestEvaluationCommand, Result<EvaluationResponseDTO>>
    {
        public async Task<Result<EvaluationResponseDTO>> Handle(GetLatestEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await evaluationRepository.FindEvaluationResponseDTOByIdAsync();
            if (evaluation is null)
                return Result.Failure<EvaluationResponseDTO>(Error.NotFound(
                    "Evaluation.NotFound", 
                    "Evaluation not found"));

            return Result.Success(evaluation);
        }
    }
}
