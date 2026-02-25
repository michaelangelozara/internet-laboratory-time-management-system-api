using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.Users
{
    public interface IUserRoleRepository
    {
        Task AddAsync(UserRole userRole);
    }
}
