using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Core.Resolving;
using CustomerManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy.DependencyInjection
{
    internal class MultiTenantContainer<T> : IContainer where T : Tenant
    {
        //This is the base application container
        private readonly IContainer m_ApplicationContainer;
        //This action configures a container builder
        private readonly Action<T, ContainerBuilder> m_TenantContainerConfiguration;

        //This dictionary keeps track of all of the tenant scopes that we have created
        private readonly Dictionary<string, ILifetimeScope> m_TenantLifetimeScopes = new();

        private readonly object m_Lock = new();
        private const string m_MultiTenantTag = "multitenantcontainer";

        public event EventHandler<LifetimeScopeBeginningEventArgs> ChildLifetimeScopeBeginning;
        public event EventHandler<LifetimeScopeEndingEventArgs> CurrentScopeEnding;
        public event EventHandler<ResolveOperationBeginningEventArgs> ResolveOperationBeginning;

        public DiagnosticListener DiagnosticSource => new("TenantContainer");

        public IDisposer Disposer => GetCurrentTenantScope().Disposer;

        public object Tag => GetCurrentTenantScope().Tag;

        public IComponentRegistry ComponentRegistry => GetCurrentTenantScope().ComponentRegistry;

        public MultiTenantContainer(IContainer applicationContainer, Action<T, ContainerBuilder> containerConfiguration)
        {
            m_TenantContainerConfiguration = containerConfiguration;
            m_ApplicationContainer = applicationContainer;
        }

        /// <summary>
        /// Get the current teanant from the application container
        /// </summary>
        /// <returns></returns>
        private T GetCurrentTenant()
            => m_ApplicationContainer.Resolve<TenantAccessService<T>>().GetTenantAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Get the scope of the current tenant
        /// </summary>
        /// <returns></returns>
        public ILifetimeScope GetCurrentTenantScope()
        {
            return GetTenantScope(GetCurrentTenant()?.Id);
        }

        /// <summary>
        /// Get (configure on missing)
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public ILifetimeScope GetTenantScope(string tenantId)
        {
            //If no tenant (e.g. early on in the pipeline, we just use the application container)
            if (tenantId == null)
                return m_ApplicationContainer;

            //If we have created a lifetime for a tenant, return
            if (m_TenantLifetimeScopes.ContainsKey(tenantId))
                return m_TenantLifetimeScopes[tenantId];

            lock (m_Lock)
            {
                if (m_TenantLifetimeScopes.ContainsKey(tenantId))
                {
                    return m_TenantLifetimeScopes[tenantId];
                }
                else
                {
                    //This is a new tenant, configure a new lifetimescope for it using our tenant sensitive configuration method
                    m_TenantLifetimeScopes.Add(tenantId, m_ApplicationContainer.BeginLifetimeScope(m_MultiTenantTag, a => m_TenantContainerConfiguration(GetCurrentTenant(), a)));
                    return m_TenantLifetimeScopes[tenantId];
                }
            }
        }


        public ILifetimeScope BeginLifetimeScope() =>
            GetCurrentTenantScope();

        public ILifetimeScope BeginLifetimeScope(object tag) =>
            GetCurrentTenantScope().BeginLifetimeScope(tag);

        public ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> configurationAction) =>
            GetCurrentTenantScope().BeginLifetimeScope(configurationAction);

        public ILifetimeScope BeginLifetimeScope(object tag, Action<ContainerBuilder> configurationAction) =>
            GetCurrentTenantScope().BeginLifetimeScope(tag, configurationAction);

        public object ResolveComponent(ResolveRequest request) =>
            GetCurrentTenantScope().ResolveComponent(request);


        public void Dispose()
        {
            lock (m_Lock)
            {
                foreach (var scope in m_TenantLifetimeScopes)
                    scope.Value.Dispose();
                m_ApplicationContainer.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var scope in m_TenantLifetimeScopes)
                await scope.Value.DisposeAsync();
            await m_ApplicationContainer.DisposeAsync();
        }
    }
}
