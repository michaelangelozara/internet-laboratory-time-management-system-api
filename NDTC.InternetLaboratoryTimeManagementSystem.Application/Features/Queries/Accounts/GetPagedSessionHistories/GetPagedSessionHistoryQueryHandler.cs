using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Accounts.GetPagedSessionHistories
{
    internal class GetPagedSessionHistoryQueryHandler(IAccountRepository accountRepository)
        : IRequestHandler<GetPagedSessionHistoryQuery, Result<PagedResult<SessionHistoryResponseDTO>>>
    {
        public async Task<Result<PagedResult<SessionHistoryResponseDTO>>> Handle(GetPagedSessionHistoryQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(await accountRepository.GetPagedAsync(request.PageNumber, request.PageSize, request.Query));
        }
    }
}
