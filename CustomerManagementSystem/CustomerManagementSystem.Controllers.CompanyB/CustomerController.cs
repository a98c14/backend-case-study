using CustomerManagementSystem.Multitenancy;
using CustomerManagementSystem.Services.CompanyB.Interfaces;
using CustomerManagementSystem.Services.CompanyB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Controllers.CompanyB
{
    [ApiController]
    [Route(TenantName.NameCompanyB + "/[controller]")]
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
            return Ok("Customer succesfully updated!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await m_CustomerService.Delete(id);
            return Ok("Customer succesfully deleted");
        }


        [HttpPost("Validate")]
        public async Task<IActionResult> ValidateGSM([FromBody] GSMValidationModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await m_CustomerService.ValidateGSM(model.Token, model.GSM); ;
            if (!result)
                return BadRequest("Invalid validation token");

            return Ok("Email succesfully validated");
        }
    }
}
