using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class ClientDeviceRepository(AppDbContext context)
        : IClientDeviceRepository
    {
        public async Task<ClientDevice?> FindByName(string deviceName)
        {
            return await context.ClientDevices
                .FirstOrDefaultAsync(cd => cd.Name == deviceName);
        }
    }
}
