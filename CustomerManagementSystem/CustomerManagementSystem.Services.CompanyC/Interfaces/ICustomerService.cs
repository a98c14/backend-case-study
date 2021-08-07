using CustomerManagementSystem.Services.CompanyC.Models;
using System.Collections.Generic;

namespace CustomerManagementSystem.Services.CompanyC.Interfaces
{
    public interface ICustomerService
    {
        public List<CustomerModel> GetAll();
        public CustomerModel GetById(int id);
        public void Create(CustomerModel model);
        public void Update(int id, CustomerModel model);
        public void Delete(int id);
    }
}
