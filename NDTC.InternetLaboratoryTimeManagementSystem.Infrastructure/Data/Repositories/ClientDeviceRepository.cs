using Microsoft.EntityFrameworkCore;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.DTOs.ClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories.ClientDevices;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal class ClientDeviceRepository(AppDbContext context)
        : BaseRepository<ClientDevice>(context), IClientDeviceRepository
    {
        public async Task<ClientDevice?> FindByNameAsNoTrackingAsync(string deviceName)
        {
            return await context.ClientDevices
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Name == deviceName);
        }
        
        public async Task<PagedResult<ClientDeviceResponseDTO>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await context.ClientDevices
                .AsNoTracking()
                .Select(cd => new ClientDeviceResponseDTO(
                    cd.Id,
                    cd.Name,
                    cd.ConnectedAt))
                .ToPagedResultAsync(pageNumber, pageSize);
        }

        public override Task AddRangeAsync(IEnumerable<ClientDevice> values)
        {
            throw new NotImplementedException();
        }

        public override void Update(ClientDevice value)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ClientDevice value) => context.ClientDevices.Remove(value);

        public async Task<ClientDevice?> FindByConnectionIdAsync(string connectionId)
        {
            return await context.ClientDevices
                .FirstOrDefaultAsync(cd => cd.ConnectionId == connectionId);
        }
    }
}
