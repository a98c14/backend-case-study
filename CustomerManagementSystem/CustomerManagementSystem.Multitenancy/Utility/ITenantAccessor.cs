using CustomerManagementSystem.Domain;

namespace CustomerManagementSystem.Multitenancy.Utility
{
    public interface ITenantAccessor<T> where T : Tenant
    {
        T Tenant { get; }
    }
}
