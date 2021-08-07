using Autofac;
using CustomerManagementSystem.Infrastructure.CompanyA;
using CustomerManagementSystem.Services.CompanyA.Interfaces;

namespace CustomerManagementSystem.Services.CompanyA
{
    public static class StartupConfiguration
    {
        public static void AddCompanyAServices(this ContainerBuilder c)
        {
            c.RegisterType<DataContext>();
            c.RegisterType<ScoringService>().As<IScoringService>().InstancePerLifetimeScope();
            c.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
        }
    }
}
