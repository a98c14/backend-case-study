using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagementSystem.Core.Storage
{
    public static class StartupConfiguration
    {
        public static void AddBlobStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
            services.AddScoped<BlobStorageService>();
        }
    }
}
