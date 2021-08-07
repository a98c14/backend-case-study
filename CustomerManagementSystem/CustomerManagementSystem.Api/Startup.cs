using Autofac;
using CustomerManagementSystem.Api.Extensions;
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Multitenancy;
using CustomerManagementSystem.Multitenancy.TenantResolution;
using CustomerManagementSystem.Services.CompanyA;
using CustomerManagementSystem.Services.CompanyB;
using CustomerManagementSystem.Services.CompanyC;
using CustomerManagementSystem.Util.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagementSystem.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostContext;
        private const string CORS_ALL = "All";

        public Startup(IConfiguration config, IHostingEnvironment hostContext)
        {
            Configuration = config;
            HostContext = hostContext;
        }

        public static void ConfigureMultiTenantServices(Tenant tenant, ContainerBuilder services)
        {
            // Configures the tenant specific services
            switch (tenant.Id) {
                case TenantId.GuidCompanyA:
                    services.AddCompanyAServices();
                    break;
                case TenantId.GuidCompanyB:
                    services.AddCompanyBServices();
                    break;
                case TenantId.GuidCompanyC:
                    services.AddCompanyCServices();
                    break;
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(CORS_ALL, build => build.AllowAnyHeader()
                                                                                  .AllowAnyOrigin()
                                                                                  .AllowAnyMethod()));
            services.AddControllers();
            services.AddSwagger();
            services.AddHttpContextAccessor();

            var assemblyCompanyA = typeof(CustomerManagementSystem.Controllers.CompanyA.CustomersController).Assembly;
            var assemblyCompanyB = typeof(CustomerManagementSystem.Controllers.CompanyB.CustomersController).Assembly;
            var assemblyCompanyC = typeof(CustomerManagementSystem.Controllers.CompanyC.CustomersController).Assembly;            

            services.AddControllers()
                .AddApplicationPart(assemblyCompanyA)
                .AddApplicationPart(assemblyCompanyB)
                .AddApplicationPart(assemblyCompanyC)
                .AddControllersAsServices();

            services.AddMultiTenancy()
                .WithResolutionStrategy<RouteResolutionStrategy>()
                .WithStore<InMemoryTenantStore>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerManagementSystem API");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();
            app.UseCors(CORS_ALL);
            app.UseMiddleware<ResponseHeaderMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMultiTenancy()
               .UseMultiTenantContainer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}