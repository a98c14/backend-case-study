using CustomerManagementSystem.Services.CompanyA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Services.CompanyA.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<CustomerModel>> GetAll();
        public Task<CustomerModel> GetById(int id);
        public Task<CustomerModel> Create(CustomerModel model);
        public Task Update(int id, CustomerModel model);
        public Task Delete(int id);
    }
}
