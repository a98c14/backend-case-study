using Microsoft.AspNetCore.Http;

namespace CustomerManagementSystem.Multitenancy.TenantResolution
{
    /// <summary>
    /// Determines the active tenant by looking at first part of the route
    /// </summary>
    public class RouteResolutionStrategy : ITenantResolutionStrategy
    {
        private readonly IHttpContextAccessor m_HttpContextAccessor;

        public RouteResolutionStrategy(IHttpContextAccessor httpContextAccessor)
        {
            m_HttpContextAccessor = httpContextAccessor;
        }

        public string GetTenantId()
        {
            var id = m_HttpContextAccessor?
                .HttpContext?
                .Request?.Path.Value.Split("/")[1] 
                ?? TenantName.Default;

            return id;
        }
    }
}
