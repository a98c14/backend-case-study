using Autofac;
using CustomerManagementSystem.Services.CompanyB.Interfaces;

namespace CustomerManagementSystem.Services.CompanyB
{
    public static class StartupConfiguration
    {
        public static void AddCompanyBServices(this ContainerBuilder c)
        {
            c.RegisterInstance(new ScoringService()).As<IScoringService>();
            c.RegisterInstance(new CustomerService()).As<ICustomerService>();
        }
    }
}
