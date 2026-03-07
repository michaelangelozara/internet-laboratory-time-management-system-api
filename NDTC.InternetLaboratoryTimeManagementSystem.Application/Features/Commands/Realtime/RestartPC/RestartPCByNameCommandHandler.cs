using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Commands.Realtime.RestartPC
{
    internal class RestartPCByNameCommandHandler(
        IClientDeviceRepository clientDeviceRepository,
        IClientDeviceHubService clientDeviceHubService)
        : IRequestHandler<RestartPCByNameCommand, Result>
    {
        public async Task<Result> Handle(RestartPCByNameCommand request, CancellationToken cancellationToken)
        {
            var clientDevice = await clientDeviceRepository.FindByName(request.DeviceName);
            if(clientDevice is null)
                return Result.Success();

            await clientDeviceHubService.PublishRestartSignalTo(clientDevice.ConnectionId);

            return Result.Success();
        }
    }
}
