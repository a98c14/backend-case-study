using CustomerManagementSystem.Domain;
using Microsoft.AspNetCore.Http;

namespace CustomerManagementSystem.Multitenancy
{
    public static class HttpContextExtensions
    {

        public static T GetTenant<T>(this HttpContext context) where T : Tenant
        {
            if (!context.Items.ContainsKey(Constants.HttpContextTenantKey))
                return null;
            return context.Items[Constants.HttpContextTenantKey] as T;
        }

        /// <summary>
        /// Returns the current Tenant
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Tenant GetTenant(this HttpContext context)
        {
            return context.GetTenant<Tenant>();
        }
    }
}
