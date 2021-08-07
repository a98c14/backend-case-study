using CustomerManagementSystem.Core.Live.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace CustomerManagementSystem.Core.Live
{
    public static class EndpointConfiguration
    {
        public static void MapCustomerManagementSystemLive(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapHub<NotificationHub>("/notifications-hub");
        }
    }
}
