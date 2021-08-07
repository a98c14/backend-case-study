using CustomerManagementSystem.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CustomerManagementSystem.Multitenancy
{
    public class TenantBuilder<T> where T : Tenant
    {
        private readonly IServiceCollection m_Services;

        public TenantBuilder(IServiceCollection services)
        {
            services.AddTransient<TenantAccessService<T>>();
            m_Services = services;
        }

        /// <summary>
        /// Register the tenant resolver implementation
        /// </summary>
        /// <returns></returns>
        public TenantBuilder<T> WithResolutionStrategy<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ITenantResolutionStrategy
        {
            m_Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            m_Services.Add(ServiceDescriptor.Describe(typeof(ITenantResolutionStrategy), typeof(V), lifetime));
            return this;
        }

        /// <summary>
        /// Register the tenant store implementation
        /// </summary>
        /// <returns></returns>
        public TenantBuilder<T> WithStore<V>(ServiceLifetime lifetime = ServiceLifetime.Transient) where V : class, ITenantStore<T>
        {
            m_Services.Add(ServiceDescriptor.Describe(typeof(ITenantStore<T>), typeof(V), lifetime));
            return this;
        }
    }
}
