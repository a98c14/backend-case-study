using Autofac;
using CustomerManagementSystem.Api.Extensions;
using CustomerManagementSystem.Domain;
using CustomerManagementSystem.Multitenancy;
using CustomerManagementSystem.Multitenancy.DependencyInjection;
using CustomerManagementSystem.Multitenancy.Options;
using CustomerManagementSystem.Multitenancy.TenantResolution;
using CustomerManagementSystem.Util.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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

        public static void ConfigureMultiTenantServices(Tenant t, ContainerBuilder c)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(CORS_ALL, build => build.AllowAnyHeader()
                                                                                  .AllowAnyOrigin()
                                                                                  .AllowAnyMethod()));
            services.AddControllers();
            services.AddSwagger();
            services.AddHttpContextAccessor();
            services.AddDistributedCache(HostContext, Configuration);

            var assemblyCompanyA = typeof(CustomerManagementSystem.Controllers.CompanyA.CustomersController).Assembly;
            var assemblyCompanyB = typeof(CustomerManagementSystem.Controllers.CompanyB.CustomersController).Assembly;
            var assemblyCompanyC = typeof(CustomerManagementSystem.Controllers.CompanyC.CustomersController).Assembly;

            services.AddControllers()
                .AddApplicationPart(assemblyCompanyA)
                .AddApplicationPart(assemblyCompanyB)
                .AddApplicationPart(assemblyCompanyC)
                .AddControllersAsServices();

            services.AddMultiTenancy()
                .WithResolutionStrategy<HeaderResolutionStrategy>()
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