using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.AnswerEvaluations.Create
{
    internal class CreateAnswerEvaluationCommandHandler(
        IEvaluationRepository evaluationRepository,
        IAnswerEvaluationRepository answerEvaluationRepository,
        IUnitOfWork unitOfWork,
        IUserContext userContext)
        : IRequestHandler<CreateAnswerEvaluationCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateAnswerEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await evaluationRepository.FindByIdAsync(request.EvaluationId);
            if (evaluation is null)
                return Result.Failure<Guid>(Error.NotFound("Evaluation.NotFound", "Evaluation not found."));

            var userId = userContext.UserId;

            bool alreadyAnswered = await answerEvaluationRepository.DoesExistByEvaluationIdAndUserIdAsync(request.EvaluationId, userId);
            if(alreadyAnswered)
                return Result.Failure<Guid>(Error.NotFound("Evaluation.Invalid", "You answered already."));

            var addingResult = evaluation.AddAnswerEvaluation(request.Comment, request.EvaluationType, userId);
            if(addingResult.IsFailure)
                return Result.Failure<Guid>(addingResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(evaluation.Id);
        }
    }
}
