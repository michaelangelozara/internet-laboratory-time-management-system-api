using Microsoft.EntityFrameworkCore;
using System.Reflection;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Aggregates;
using NDTC.InternetLaboratoryTimeManagementSystem.Domain.Entities;

namespace NDTC.InternetLaboratoryTimeManagementSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; private set; }

        public DbSet<UserRole> UserRoles { get; private set; }

        public DbSet<Account> Accounts { get; private set; }

        public DbSet<Role> Roles { get; private set; }

        public DbSet<RoleClaim> RoleClaims { get; private set; }

        public DbSet<Evaluation> Evaluations { get; private set; }

        public DbSet<AnswerEvaluation> AnswerEvaluations { get; private set; }

        public DbSet<SyncRequest> SyncRequests { get; private set; }

        public DbSet<SyncLock> SyncLocks { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // apply all derived classes of IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
