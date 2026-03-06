using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Realtime.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class AccountService(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        ISessionHubService sessionHubService)
        : IAccountService
    {
        public async Task LogoutAllAccounts()
        {
            await unitOfWork.BeginTransactionAsync();

            await accountRepository.SetIsLoggedInToFalseAndReComputeAvailableDurationAsync();

            await unitOfWork.CommitAsync();
        }

        public async Task ResetAllAccountDurations()
        {
            await accountRepository.ResetAllAccountDurationsAsync();
        }

        public async Task TerminateInvalidSessions()
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();
                var accounts = await accountRepository.FindLoggedInAccountsByCountAsync(20);
                foreach(var account in accounts)
                {
                    if (account.RemainingDurationTimeSpan <= TimeSpan.Zero)
                    {
                        account.LogOut();
                        await sessionHubService.PublishTerminationTo(account.UserId);
                        await sessionHubService.PublishTerminatedUserId(account.UserId);
                    }
                }

                await unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
