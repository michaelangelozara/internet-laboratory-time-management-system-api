using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Authentication;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Evaluations.Create
{
    internal class CreateEvaluationCommandHandler(
        IUnitOfWork unitOfWork,
        IEvaluationRepository evaluationRepository,
        IUserContext userContext)
        : IRequestHandler<CreateEvaluationCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateEvaluationCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            await evaluationRepository.DeactivateAllEvaluations();

            var evaluation = Evaluation.Create(request.Question, userContext.UserId);
            await evaluationRepository.AddAsync(evaluation);

            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(evaluation.Id);
        }
    }
}
