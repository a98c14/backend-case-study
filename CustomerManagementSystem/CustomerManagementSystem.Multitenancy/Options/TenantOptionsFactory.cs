using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Multitenancy.Utility;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Multitenancy.Options
{
    /// <summary>
    /// Create a new options instance with configuration applied
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="T"></typeparam>
    internal class TenantOptionsFactory<TOptions, T> : IOptionsFactory<TOptions>
        where TOptions : class, new()
        where T : Tenant
    {

        private readonly IEnumerable<IConfigureOptions<TOptions>> m_Setups;
        private readonly IEnumerable<IPostConfigureOptions<TOptions>> m_PostConfigures;
        private readonly Action<TOptions, T> m_TenantConfig;
        private readonly ITenantAccessor<T> m_TenantAccessor;

        public TenantOptionsFactory(
            IEnumerable<IConfigureOptions<TOptions>> setups,
            IEnumerable<IPostConfigureOptions<TOptions>> postConfigures, Action<TOptions, T> tenantConfig, ITenantAccessor<T> tenantAccessor)
        {
            m_Setups = setups;
            m_PostConfigures = postConfigures;
            m_TenantAccessor = tenantAccessor;
            m_TenantConfig = tenantConfig;
        }

        /// <summary>
        /// Create a new options instance
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TOptions Create(string name)
        {
            var options = new TOptions();

            //Apply options setup configuration
            foreach (var setup in m_Setups)
            {
                if (setup is IConfigureNamedOptions<TOptions> namedSetup)
                {
                    namedSetup.Configure(name, options);
                }
                else
                {
                    setup.Configure(options);
                }
            }

            //Apply tenant specifc configuration (to both named and non-named options)
            if (m_TenantAccessor.Tenant != null)
                m_TenantConfig(options, m_TenantAccessor.Tenant);

            //Apply post configuration
            foreach (var postConfig in m_PostConfigures)
            {
                postConfig.PostConfigure(name, options);
            }

            return options;
        }
    } 
}
