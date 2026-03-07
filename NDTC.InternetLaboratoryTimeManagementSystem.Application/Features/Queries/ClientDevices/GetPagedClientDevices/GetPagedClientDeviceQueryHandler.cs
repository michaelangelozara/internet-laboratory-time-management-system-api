using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.ClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.ClientDevices.GetPagedClientDevices
{
    internal class GetPagedClientDeviceQueryHandler(IClientDeviceRepository clientDeviceRepository)
        : IRequestHandler<GetPagedClientDeviceQuery, Result<PagedResult<ClientDeviceResponseDTO>>>
    {
        public async Task<Result<PagedResult<ClientDeviceResponseDTO>>> Handle(GetPagedClientDeviceQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(await clientDeviceRepository.GetPagedAsync(request.PageNumber, request.PageSize));
        }
    }
}
