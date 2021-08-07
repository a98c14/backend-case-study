using Microsoft.AspNetCore.Http;

namespace CustomerManagementSystem.Multitenancy.TenantResolution
{
    /// <summary>
    /// Determines the active tenant by looking at the domain host 
    /// </summary>
    public class HostResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;

        public HostResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            m_HttpContextAccessor = httpContextAccessor;
        }

        public string GetTenantId()
        {
            return m_HttpContextAccessor?.HttpContext?.Request?.Host.Host ?? TenantName.Default;
        }
    }
}
