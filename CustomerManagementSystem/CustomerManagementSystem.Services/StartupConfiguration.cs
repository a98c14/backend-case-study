using CustomerManagementSystem.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagementSystem.Services
{
    public static class StartupConfiguration
    {
        public static void AddDefaultServices(this IServiceCollection services)
        {
            services.AddScoped<IMernisValidationService, MernisValidationService>();
            services.AddScoped<IEmailValidationService, EmailValidationService>();
            services.AddScoped<IOneTimePinService, OneTimePinService>();
        }
    }
}
