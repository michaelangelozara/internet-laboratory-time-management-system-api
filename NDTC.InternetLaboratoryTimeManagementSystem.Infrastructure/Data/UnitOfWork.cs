using Microsoft.EntityFrameworkCore.Storage;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Repositories;
using NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Extensions;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data
{
    internal class UnitOfWork(AppDbContext context)
        : IUnitOfWork
    {
        private IDbContextTransaction? dbContextTransaction;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (dbContextTransaction != null)
                return; // Already started

            dbContextTransaction = await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync(cancellationToken); // Save pending changes
                if (dbContextTransaction != null)
                {
                    await dbContextTransaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                await RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (dbContextTransaction != null)
            {
                await dbContextTransaction.RollbackAsync(cancellationToken);
                await DisposeTransactionAsync();
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            context.ApplyAuditing();

            var result = await context.SaveChangesAsync(cancellationToken);

            return result;
        }

        private async Task DisposeTransactionAsync()
        {
            if (dbContextTransaction != null)
            {
                await dbContextTransaction.DisposeAsync();
                dbContextTransaction = null;
            }
        }
    }
}
