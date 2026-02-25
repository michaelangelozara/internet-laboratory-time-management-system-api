using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Roles
{
    public interface IRoleRepository
    {
        Task AddAsync(Role role);

        Task<Role?> FindByNameAsNoTrackingAsync(string roleName);

        Task<Role?> FindByNameAsync(string roleName);
    }
}
