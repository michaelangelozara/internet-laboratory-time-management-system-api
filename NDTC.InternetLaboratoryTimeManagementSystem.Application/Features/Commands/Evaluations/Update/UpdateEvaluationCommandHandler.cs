using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Evaluations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Evaluations.Update
{
    internal class UpdateEvaluationCommandHandler(
        IEvaluationRepository evaluationRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateEvaluationCommand, Result>
    {
        public async Task<Result> Handle(UpdateEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await evaluationRepository.FindByIdAsync(request.Id);
            if (evaluation is null)
                return Result.Failure(Error.NotFound("Evaluation.NotFound", "Evaluation not found."));

            evaluation.SetQuestion(request.Question);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
