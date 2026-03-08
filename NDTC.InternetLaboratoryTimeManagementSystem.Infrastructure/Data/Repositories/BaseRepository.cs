using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data.Repositories
{
    internal abstract class BaseRepository<TEntity> (AppDbContext context)
        : IBaseRepository<TEntity> where TEntity : Entity
    {
        public async Task AddAsync(TEntity value)
        {
            await context.AddAsync(value);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> values)
        {
            await context.AddRangeAsync(values);
        }

        public virtual void Remove(TEntity value)
        {
            context.Remove(value);
        }

        public virtual void Update(TEntity value)
        {
            context.Update(value);
        }
    }
}
