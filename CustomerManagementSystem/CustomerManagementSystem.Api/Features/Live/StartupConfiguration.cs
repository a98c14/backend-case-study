using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagementSystem.Core.Live
{
    public static class StartupConfiguration
    {
        public static void AddCustomerManagementSystemLive(this IServiceCollection services)
        {
            services.AddSignalR();
        }
    }
}
