using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using CustomerManagementSystem.Domain.CompanyC;

namespace CustomerManagementSystem.Infrastructure.CompanyC
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> Accounts { get; set; }

        private readonly IConfiguration m_Configuration;

        public DataContext(IConfiguration configuration)
        {
            m_Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseOracle(m_Configuration.GetConnectionString("Development.CompanyA"));
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is Customer && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Customer)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((Customer)entityEntry.Entity).CreatedAt = DateTime.Now;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.Entity is Customer && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Customer)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                    ((Customer)entityEntry.Entity).CreatedAt = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}