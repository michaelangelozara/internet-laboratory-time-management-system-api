using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Evaluations.Create
{
    public sealed record CreateEvaluationCommand(string Question) : IRequest<Result<Guid>>;
}
