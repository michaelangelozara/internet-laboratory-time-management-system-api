using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Realtime.ClientDevices.RestartPC
{
    public sealed record RestartPCByNameCommand(string DeviceName) : IRequest<Result>;
}
