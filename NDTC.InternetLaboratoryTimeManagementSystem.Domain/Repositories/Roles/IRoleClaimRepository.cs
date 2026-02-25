using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Roles
{
    public interface IRoleClaimRepository
    {
        Task AddAsync(RoleClaim roleClaim);

        Task<HashSet<string>> FindClaimsByUserId(Guid userId);
    }
}
