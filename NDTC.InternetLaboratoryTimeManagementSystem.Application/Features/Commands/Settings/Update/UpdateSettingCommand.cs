using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Settings.Update
{
    public sealed record UpdateSettingCommand(bool IsSyncing)
        : IRequest<Result>;
}
