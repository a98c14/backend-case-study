namespace CustomerManagementSystem.Multitenancy
{
    public interface ITenantResolutionStrategy
    {
        string GetTenantId();
    }
}
