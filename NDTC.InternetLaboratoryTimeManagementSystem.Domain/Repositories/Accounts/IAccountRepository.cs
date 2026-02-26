using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts
{
    public interface IAccountRepository
    {
        Task<Account?> FindByRFIDWithUserAsync(string rfid);

        Task<Account?> FindByUserIdAsync(Guid userId);

        Task SetIsLoggedInToFalseAndReComputeAvailableDurationAsync();

        Task ResetAllAccountDurationsAsync();

        Task<Account?> FindBySchoolIdAsync(string schoolId);
    }
}
