using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.AnswerEvaluations.Create
{
    public sealed record CreateAnswerEvaluationCommand(
        Guid EvaluationId,
        string Comment,
        string EvaluationType) 
        : IRequest<Result<Guid>>;
}
