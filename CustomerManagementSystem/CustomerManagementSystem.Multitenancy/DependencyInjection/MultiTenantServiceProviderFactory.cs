using Autofac;
using Autofac.Extensions.DependencyInjection;
using CustomerManagementSystem.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy.DependencyInjection
{
    public class MultiTenantServiceProviderFactory<T> : IServiceProviderFactory<ContainerBuilder> where T : Tenant
    {
        public Action<T, ContainerBuilder> TenantServicesConfiguration;

        public MultiTenantServiceProviderFactory(Action<T, ContainerBuilder> tenantServicesConfiguration)
        {
            TenantServicesConfiguration = tenantServicesConfiguration;
        }

        /// <summary>
        /// Create a builder populated with global services
        /// </summary>
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            return builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            MultiTenantContainer<T> container = null;

            Func<MultiTenantContainer<T>> containerAccessor = () =>
            {
                return container;
            };

            containerBuilder
                .RegisterInstance(containerAccessor)
                .SingleInstance();

            container = new MultiTenantContainer<T>(containerBuilder.Build(), TenantServicesConfiguration);

            return new AutofacServiceProvider(containerAccessor());
        }
    }

}
