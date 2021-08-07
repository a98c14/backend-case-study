using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using CustomerManagementSystem.Domain.CompanyB;

namespace CustomerManagementSystem.Infrastructure.CompanyB
{
    public class DataContext : DbContext
    {
        public DbSet<Customer> Accounts { get; set; }

        private readonly IConfiguration m_Configuration;
        private readonly IHostEnvironment m_Env;

        public DataContext(IConfiguration configuration, IHostEnvironment env)
        {
            m_Configuration = configuration;
            m_Env = env;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(m_Configuration.GetConnectionString(m_Env.IsDevelopment() ? "Development.CompanyB" : "Production.CompanyB"));
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