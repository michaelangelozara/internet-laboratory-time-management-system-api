using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Realtime.RestartPC
{
    public sealed record RestartPCByUserIdCommand(Guid UserId) : IRequest<Result>;
}
