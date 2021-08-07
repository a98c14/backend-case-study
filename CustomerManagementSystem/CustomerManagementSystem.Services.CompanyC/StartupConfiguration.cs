using Autofac;
using CustomerManagementSystem.Services.CompanyC.Interfaces;

namespace CustomerManagementSystem.Services.CompanyC
{
    public static class StartupConfiguration
    {
        public static void AddCompanyCServices(this ContainerBuilder c)
        {
            c.RegisterInstance(new ScoringService()).As<IScoringService>();
            c.RegisterInstance(new CustomerService()).As<ICustomerService>();
        }
    }
}
