using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Accounts.GetPagedSessionHistories
{
    public sealed record GetPagedSessionHistoryQuery(int PageNumber, int PageSize, string? Query)
        : IRequest<Result<PagedResult<SessionHistoryResponseDTO>>>;
}
