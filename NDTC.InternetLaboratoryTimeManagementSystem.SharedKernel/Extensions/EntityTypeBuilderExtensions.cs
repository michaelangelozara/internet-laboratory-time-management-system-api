using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Abstractions.Configurations;
using NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Configurations;

namespace NDTC.InternetLaboratoryTimeManagementSystem.SharedKernel.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ApplyEntityBase<TEntity>(
        this EntityTypeBuilder<TEntity> builder)
        where TEntity : Entity
        {
            new EntityConfiguration<TEntity>()
                .Configure(builder);
        }

        public static void ApplyAuditing<TEntity>(
            this EntityTypeBuilder<TEntity> builder)
            where TEntity : Entity, IAuditable
        {
            new AuditableEntityConfiguration<TEntity>()
                .Configure(builder);
        }
    }
}
