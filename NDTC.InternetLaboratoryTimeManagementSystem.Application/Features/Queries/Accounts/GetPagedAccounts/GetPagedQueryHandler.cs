using MediatR;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Application.Features.Queries.Accounts.GetPagedAccounts
{
    internal class GetPagedQueryHandler(IAccountRepository accountRepository)
        : IRequestHandler<GetPagedAccountQuery, Result<PagedResult<AccountResponseDTO>>>
    {
        public async Task<Result<PagedResult<AccountResponseDTO>>> Handle(GetPagedAccountQuery request, CancellationToken cancellationToken)
        {
            return Result.Success(
                await accountRepository.GetPagedAsync(
                    request.PageNumber,
                    request.PageSize, 
                    request.Active));
        }
    }
}
