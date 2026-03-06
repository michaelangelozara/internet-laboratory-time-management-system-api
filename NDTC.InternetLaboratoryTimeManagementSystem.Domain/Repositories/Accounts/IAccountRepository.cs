using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts
{
    public interface IAccountRepository 
        : IPagedRepository<AccountResponseDTO, bool?>, IPagedRepository<SessionHistoryResponseDTO>
    {
        Task<Account?> FindByRFIDWithUserAsync(string rfid);

        Task<Account?> FindByUserIdAsync(Guid userId);

        Task SetIsLoggedInToFalseAndReComputeAvailableDurationAsync();

        Task ResetAllAccountDurationsAsync();

        Task<Account?> FindBySchoolIdAsync(string schoolId);

        Task<Account?> FindByUserIdWithUserAsNoTrackingAsync(Guid userId);

        Task<IEnumerable<Account>> FindLoggedInAccountsByCountAsync(int count);

        Task<bool> IsSignedInByUserIdAsync(Guid userId);

        Task<bool> DoesExistByUserIdAsync(Guid userId);
    }
}
