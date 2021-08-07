using CustomerManagementSystem.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy
{
    public class InMemoryTenantStore : ITenantStore<Tenant>
    {
        public async Task<Tenant> GetTenantAsync(string name)
        {
            var tenant = new[]
            {
                new Tenant{ Id = TenantId.Default,      Name = "default" },
                new Tenant{ Id = TenantId.GuidCompanyA, Name = "company-a" },
                new Tenant{ Id = TenantId.GuidCompanyB, Name = "company-b" },
                new Tenant{ Id = TenantId.GuidCompanyC, Name = "company-c" },
            }.SingleOrDefault(t => t.Name == name.ToLower()) ?? new Tenant { Id = TenantId.Default, Name = "default" };
            return await Task.FromResult(tenant);
        }
    }
}
