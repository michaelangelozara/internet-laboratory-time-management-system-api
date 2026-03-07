using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.ClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.ClientDevices.GetPagedClientDevices
{
    public sealed record GetPagedClientDeviceQuery(
        int PageNumber,
        int PageSize) : IRequest<Result<PagedResult<ClientDeviceResponseDTO>>>;
}
