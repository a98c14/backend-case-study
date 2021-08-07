using Autofac;
using CustomerManagementSystem.Services.CompanyA.Interfaces;

namespace CustomerManagementSystem.Services.CompanyA
{
    public static class StartupConfiguration
    {
        public static void AddCompanyAServices(this ContainerBuilder c)
        {
            c.RegisterInstance(new ScoringService()).As<IScoringService>();
            c.RegisterInstance(new CustomerService()).As<ICustomerService>();
        }
    }
}
