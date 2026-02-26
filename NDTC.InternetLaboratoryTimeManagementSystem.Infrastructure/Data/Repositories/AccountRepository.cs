using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class AccountRepository(
        AppDbContext context,
        IDateTimeProvider dateTimeProvider)
        : IAccountRepository
    {
        public async Task<Account?> FindByRFIDWithUserAsync(string rfid)
        {
            return await context.Accounts
                .Where(a => a.RFID == rfid)
                .Include(a => a.User)
                    .ThenInclude(u => u.UserRoles)
                        .ThenInclude(ur => ur.Role)
                            .ThenInclude(r => r.RoleClaims)
                .FirstOrDefaultAsync();
        }

        public async Task<Account?> FindByUserIdAsync(Guid userId)
        {
            return await context.Accounts
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task SetIsLoggedInToFalseAndReComputeAvailableDuration()
        {
            await context.Accounts
                .Where(a => a.IsLoggedIn)
                .ExecuteUpdateAsync(setter =>
                    setter
                        .SetProperty(a => a.IsLoggedIn, false)
                        .SetProperty(a => a.AvailableDuration, a =>
                            a.AvailableDuration - (dateTimeProvider.UtcNow - a.LastLoginAt)));
        }
    }
}
