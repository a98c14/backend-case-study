using CustomerManagementSystem.Domain;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy
{
    public class TenantAccessService<T> where T : Tenant
    {
        private readonly ITenantResolutionStrategy m_TenantResolutionStrategy;
        private readonly ITenantStore<T> m_TenantStore;

        public TenantAccessService(ITenantResolutionStrategy tenantResolutionStrategy, ITenantStore<T> tenantStore)
        {
            m_TenantResolutionStrategy = tenantResolutionStrategy;
            m_TenantStore = tenantStore;
        }

        public async Task<T> GetTenantAsync()
        {
            var id = m_TenantResolutionStrategy.GetTenantId();
            return await m_TenantStore.GetTenantAsync(id);
        }
    }
}
