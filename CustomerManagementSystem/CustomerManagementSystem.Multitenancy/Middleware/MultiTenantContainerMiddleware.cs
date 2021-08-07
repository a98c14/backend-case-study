using Autofac.Extensions.DependencyInjection;
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Multitenancy.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy.Middleware
{
    internal class MultiTenantContainerMiddleware<T> where T : Tenant
    {
        private readonly RequestDelegate next;

        public MultiTenantContainerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, Func<MultiTenantContainer<T>> multiTenantContainerAccessor)
        {
            //Set to current tenant container.
            //Begin new scope for request as ASP.NET Core standard scope is per-request
            context.RequestServices =
                new AutofacServiceProvider(multiTenantContainerAccessor()
                        .GetCurrentTenantScope().BeginLifetimeScope());
            await next.Invoke(context);
        }
    }
}
