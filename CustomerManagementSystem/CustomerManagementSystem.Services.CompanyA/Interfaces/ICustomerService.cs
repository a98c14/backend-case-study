using CustomerManagementSystem.Services.CompanyA.Models;
using System.Collections.Generic;

namespace CustomerManagementSystem.Services.CompanyA.Interfaces
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
