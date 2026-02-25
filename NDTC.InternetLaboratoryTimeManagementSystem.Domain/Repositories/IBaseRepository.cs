using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories
{
    public interface IBaseRepository<TEntity>
    where TEntity : Entity
    {
        Task AddAsync(TEntity value);

        Task AddRangeAsync(IEnumerable<TEntity> values);

        void Update(TEntity value);

        void Remove(TEntity value);
    }
}
