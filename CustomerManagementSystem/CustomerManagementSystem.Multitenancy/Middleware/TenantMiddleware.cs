using CustomerManagementSystem.Domain;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy.Middleware
{
    internal class TenantMiddleware<T> where T : Tenant
    {
        private readonly RequestDelegate next;

        public TenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Items.ContainsKey(Constants.HttpContextTenantKey))
            {
                var tenantService = context.RequestServices.GetService(typeof(TenantAccessService<T>)) as TenantAccessService<T>;
                context.Items.Add(Constants.HttpContextTenantKey, await tenantService.GetTenantAsync());
            }

            if (next != null)
                await next(context);
        }
    }
}
