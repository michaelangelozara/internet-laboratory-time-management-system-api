using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Accounts;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class AccountRepository(AppDbContext context)
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
    }
}
