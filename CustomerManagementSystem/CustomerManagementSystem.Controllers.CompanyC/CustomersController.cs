using CustomerManagementSystem.Multitenancy;
using CustomerManagementSystem.Services.CompanyC.Interfaces;
using CustomerManagementSystem.Services.CompanyC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Controllers.CompanyC
{
    [ApiController]
    [Route(TenantName.NameCompanyC + "/[controller]")]
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
            var users = await m_CustomerService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await m_CustomerService.GetById(id);
            return Ok(user);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CustomerModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await m_CustomerService.Create(model);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await m_CustomerService.Update(id, model);
            return Ok("Customer successfully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            m_CustomerService.Delete(id);
            return Ok("Customer successfully deleted");
        }

        [HttpPost("Validate")]
        public async Task<IActionResult> ValidateEmail([FromBody] EmailValidationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await m_CustomerService.ValidateEmail(model.Token, model.Email);;
            if(!result)
                return BadRequest("Invalid validation token");

            return Ok("Email succesfully validated");
        }
    }
}
