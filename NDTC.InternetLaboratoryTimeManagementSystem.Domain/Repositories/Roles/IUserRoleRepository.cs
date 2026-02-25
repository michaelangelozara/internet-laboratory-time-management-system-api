using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Roles
{
    public interface IUserRoleRepository
    {
        Task AddAsync(UserRole userRole);
    }
}
