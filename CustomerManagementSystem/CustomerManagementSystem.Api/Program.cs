using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Multitenancy.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host
              .CreateDefaultBuilder(args)
              .UseServiceProviderFactory(new MultiTenantServiceProviderFactory<Tenant>(Startup.ConfigureMultiTenantServices))
              .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
              .Build();

            await host.RunAsync();
        }
    }
}
