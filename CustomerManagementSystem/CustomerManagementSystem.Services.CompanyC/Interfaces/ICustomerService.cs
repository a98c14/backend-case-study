using CustomerManagementSystem.Services.CompanyC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Services.CompanyC.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> GetAll();
        Task<CustomerModel> GetById(int id);
        Task<CustomerModel> Create(CustomerModel model);
        Task Update(int id, CustomerModel model);
        Task Delete(int id);
        Task<bool> ValidateEmail(string token, string email);
    }
}
