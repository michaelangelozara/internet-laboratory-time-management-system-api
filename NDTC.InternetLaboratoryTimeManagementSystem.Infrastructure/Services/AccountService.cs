using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class AccountService(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork)
        : IAccountService
    {
        public async Task LogoutAllAccounts()
        {
            await unitOfWork.BeginTransactionAsync();

            await accountRepository.SetIsLoggedInToFalseAndReComputeAvailableDuration();

            await unitOfWork.CommitAsync();
        }
    }
}
