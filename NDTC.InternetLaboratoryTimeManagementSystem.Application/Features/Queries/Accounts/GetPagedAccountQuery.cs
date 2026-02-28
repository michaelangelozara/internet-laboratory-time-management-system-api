using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Accounts
{
    public sealed record GetPagedAccountQuery(int PageNumber, int PageSize, bool? Active) 
        : IRequest<Result<PagedResult<AccountResponseDTO>>>;
}
