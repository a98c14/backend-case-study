using Microsoft.AspNetCore.Http;

namespace CustomerManagementSystem.Multitenancy.TenantResolution
{
    /// <summary>
    /// Determines the active tenant by looking at the domain host 
    /// </summary>
    public class HeaderResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;

        public HeaderResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            m_HttpContextAccessor = httpContextAccessor;
        }

        public string GetTenantId()
        {
            var id = m_HttpContextAccessor?
                .HttpContext?
                .Request?
                .Headers[Constants.HttpContextTenantHeader] ?? TenantId.Default;

            return id;
        }
    }
}
