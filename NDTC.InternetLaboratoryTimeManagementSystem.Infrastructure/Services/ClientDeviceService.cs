using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Application.Abstractions.Services;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Services
{
    internal class ClientDeviceService(
        AppDbContext context)
        : IClientDeviceService
    {
        public async Task RegisterDevice(string name, string connectionId)
        {
            var clientDevice = ClientDevice.Create(name, connectionId);
            await context.ClientDevices.AddAsync(clientDevice);

            await context.SaveChangesAsync();
        }

        public async Task UnregisterDevice(string connectionId)
        {
            var clientDevice = await context.ClientDevices
                .FirstOrDefaultAsync(cd => cd.ConnectionId == connectionId);
            
            if (clientDevice is null)
                return;

            context.ClientDevices.Remove(clientDevice);

            await context.SaveChangesAsync();
        }
    }
}
