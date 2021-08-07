using CustomerManagementSystem.Domain;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy
{
    public interface ITenantStore<T> where T : Tenant
    {
        Task<T> GetTenantAsync(string id);
    }
}
