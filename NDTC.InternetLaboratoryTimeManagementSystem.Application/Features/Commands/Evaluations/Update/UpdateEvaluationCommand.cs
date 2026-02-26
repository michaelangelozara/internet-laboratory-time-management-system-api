using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Evaluations.Update
{
    public sealed record UpdateEvaluationCommand(Guid Id, string Question) : IRequest<Result>;
}
