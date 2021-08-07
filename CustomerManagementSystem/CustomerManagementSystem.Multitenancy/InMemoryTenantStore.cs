using CustomerManagementSystem.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy
{
    public class InMemoryTenantStore : ITenantStore<Tenant>
    {
        public async Task<Tenant> GetTenantAsync(string id)
        {
            var tenant = new[]
            {
                new Tenant{ Id = TenantId.Default,      Name = "Default" },
                new Tenant{ Id = TenantId.GuidCompanyA, Name = "CompanyA" },
                new Tenant{ Id = TenantId.GuidCompanyB, Name = "CompanyB" },
                new Tenant{ Id = TenantId.GuidCompanyC, Name = "CompanyC" },
            }.SingleOrDefault(t => t.Id == id) ?? new Tenant { Id = TenantId.Default, Name = "Default" };
            return await Task.FromResult(tenant);
        }
    }
}
