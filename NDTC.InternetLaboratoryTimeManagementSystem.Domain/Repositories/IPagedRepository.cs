using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories
{
    public interface IPagedRepository<T>
    {
        Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize);
    }

    public interface IPagedRepository<T, S>
    {
        Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize, S sortingObject);
    }
}
