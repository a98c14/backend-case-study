using Autofac;
using CustomerManagementSystem.Infrastructure.CompanyC;
using CustomerManagementSystem.Services.CompanyC.Interfaces;

namespace CustomerManagementSystem.Services.CompanyC
{
    public static class StartupConfiguration
    {
        public static void AddCompanyCServices(this ContainerBuilder c)
        {
            c.RegisterType<DataContext>();
            c.RegisterType<ScoringService>().As<IScoringService>();
            c.RegisterType<CustomerService>().As<ICustomerService>();
        }
    }
}
