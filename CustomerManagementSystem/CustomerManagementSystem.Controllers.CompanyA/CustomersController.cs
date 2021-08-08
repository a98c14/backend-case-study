using CustomerManagementSystem.Multitenancy;
using CustomerManagementSystem.Services.CompanyA.Interfaces;
using CustomerManagementSystem.Services.CompanyA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Controllers.CompanyA
{
    [ApiController]
    [Route(TenantName.NameCompanyA + "/[controller]")]
    public class CustomersController : ControllerBase
    {     
        private readonly ILogger<CustomersController> m_Logger;
        private readonly ICustomerService m_CustomerService;

        public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService)
        {
            m_Logger = logger;
            m_CustomerService = customerService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await m_CustomerService.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await m_CustomerService.GetById(id);
            return Ok(customer);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CustomerModel model)
        {
            var customer = await m_CustomerService.Create(model);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerModel model)
        {
            await m_CustomerService.Update(id, model);
            return Ok("Customer successfully updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await m_CustomerService.Delete(id);
            return Ok("Customer successfully deleted");
        }
    }
}
