using Microsoft.Extensions.Options;

namespace CustomerManagementSystem.Multitenancy.Options
{
    public class TenantOptions<TOptions> :
        IOptions<TOptions>, IOptionsSnapshot<TOptions> where TOptions : class, new()
    {
        private readonly IOptionsFactory<TOptions> m_Factory;
        private readonly IOptionsMonitorCache<TOptions> m_Cache;

        public TenantOptions(IOptionsFactory<TOptions> factory, IOptionsMonitorCache<TOptions> cache)
        {
            m_Factory = factory;
            m_Cache = cache;
        }

        public TOptions Value => Get(Constants.OptionsDefaultName);

        public TOptions Get(string name)
        {
            return m_Cache.GetOrAdd(name, () => m_Factory.Create(name));
        }
    }
}
