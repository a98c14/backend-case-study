using Autofac;
using CustomerManagementSystem.Infrastructure.CompanyB;
using CustomerManagementSystem.Services.CompanyB.Interfaces;

namespace CustomerManagementSystem.Services.CompanyB
{
    public static class StartupConfiguration
    {
        public static void AddCompanyBServices(this ContainerBuilder c)
        {
            c.RegisterType<DataContext>();
            c.RegisterType<ScoringService>().As<IScoringService>();
            c.RegisterType<CustomerService>().As<ICustomerService>();
        }
    }
}
