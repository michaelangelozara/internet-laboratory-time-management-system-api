using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Constants;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class AccountRepository(
        AppDbContext context,
        IDateTimeProvider dateTimeProvider)
        : IAccountRepository
    {
        public async Task<Account?> FindBySchoolIdAsync(string schoolId)
        {
            return await context.Accounts
                .FirstOrDefaultAsync(a => a.User.SchoolId == schoolId);
        }

        public async Task<Account?> FindByRFIDWithUserAsync(string rfid)
        {
            return await context.Accounts
                .AsSplitQuery()
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

        public async Task ResetAllAccountDurationsAsync()
        {
            await context.Accounts
                .Where(a => a.LastLoginAt < dateTimeProvider.UtcNow.Date)
                .ExecuteUpdateAsync(setter =>
                    setter
                        .SetProperty(a => a.AvailableDuration, Duration.DefaultAccountDuration));
        }

        public async Task SetIsLoggedInToFalseAndReComputeAvailableDurationAsync()
        {
            var now = dateTimeProvider.UtcNow;

            await context.Accounts
                .Where(a => a.IsLoggedIn)
                .ExecuteUpdateAsync(setter =>
                    setter
                        .SetProperty(a => a.IsLoggedIn, false)
                        .SetProperty(a => a.AvailableDuration, a =>
                            a.AvailableDuration -
                            (long)((now - a.LastLoginAt).TotalMilliseconds * TimeSpan.TicksPerMillisecond)));
        }

        public async Task<Account?> FindByUserIdWithUserAsNoTrackingAsync(Guid userId)
        {
            return await context.Accounts
                .AsNoTracking()
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<IEnumerable<Account>> FindLoggedInAccountsByCountAsync(int count)
        {
            return await context.Accounts
                .Where(a => a.IsLoggedIn)
                .Take(count)
                .OrderByDescending(a => a.LastLoginAt)
                .ToListAsync();
        }

        public async Task<PagedResult<AccountResponseDTO>> GetPagedAsync(int pageNumber, int pageSize, bool? active)
        {
            var accounts = context.Accounts
                .AsNoTracking()
                .Where(a => a.User != null && !a.User.UserRoles.Any());

            if(active.HasValue)
            {
                if (active.Value)
                {
                    return await accounts.Where(a => a.IsLoggedIn)
                        .Select(a => new AccountResponseDTO(
                            a.Id,
                            a.UserId,
                            a.User!.SchoolId,
                            a.IsLoggedIn,
                            a.AvailableDuration,
                            a.LastLoginAt))
                            .ToPagedResultAsync(pageNumber, pageSize);
                }
                else
                {
                    return await accounts.Where(a => !a.IsLoggedIn)
                        .Select(a => new AccountResponseDTO(
                            a.Id,
                            a.UserId,
                            a.User!.SchoolId,
                            a.IsLoggedIn,
                            a.AvailableDuration,
                            a.LastLoginAt))
                            .ToPagedResultAsync(pageNumber, pageSize);
                }
            }

            return await accounts.Select(a => new AccountResponseDTO(
                a.Id,
                a.UserId,
                a.User!.SchoolId,
                a.IsLoggedIn,
                a.AvailableDuration,
                a.LastLoginAt))
                .ToPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<bool> IsSignedInByUserIdAsync(Guid userId)
        {
            return await context.Accounts
                .AnyAsync(a => a.UserId == userId && a.IsLoggedIn);
        }

        public async Task<bool> DoesExistByUserIdAsync(Guid userId)
        {
            return await context.Accounts
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<PagedResult<SessionHistoryResponseDTO>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await context.SessionHistories
                .GroupBy(sh => new { sh.AccountId, sh.Account.User.SchoolId })
                .Select(group => new
                {
                    group.Key.SchoolId,
                    ConsumedTime = group.Sum(x => x.ConsumedTime)
                })
                .OrderBy(x => x.SchoolId)
                .Select(x => new SessionHistoryResponseDTO(
                    x.SchoolId,
                    x.ConsumedTime))
                .ToPagedResultAsync(pageNumber, pageSize);
        }
    }
}
