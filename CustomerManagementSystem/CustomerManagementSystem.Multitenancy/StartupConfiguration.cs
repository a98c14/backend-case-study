using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Multitenancy.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagementSystem.Multitenancy
{
    public static class StartupConfiguration
    {
        public static TenantBuilder<T> AddMultiTenancy<T>(this IServiceCollection services) where T : Tenant
            => new(services);

        public static TenantBuilder<Tenant> AddMultiTenancy(this IServiceCollection services)
            => new(services);

        public static IApplicationBuilder UseMultiTenancy<T>(this IApplicationBuilder builder) where T : Tenant
            => builder.UseMiddleware<TenantMiddleware<T>>();

        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
            => builder.UseMiddleware<TenantMiddleware<Tenant>>();

        public static IApplicationBuilder UseMultiTenantContainer<T>(this IApplicationBuilder builder) where T : Tenant
            => builder.UseMiddleware<MultiTenantContainerMiddleware<T>>();

        public static IApplicationBuilder UseMultiTenantContainer(this IApplicationBuilder builder)
            => builder.UseMiddleware<MultiTenantContainerMiddleware<Tenant>>();
    }
}
