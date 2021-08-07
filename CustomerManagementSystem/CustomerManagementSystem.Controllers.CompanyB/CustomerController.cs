using CustomerManagementSystem.Services.CompanyB.Interfaces;
using CustomerManagementSystem.Services.CompanyB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagementSystem.Controllers.CompanyB
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult GetAll()
        {
            m_CustomerService.GetAll();
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            m_CustomerService.GetById(id);
            return Ok();
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CustomerModel model)
        {
            m_CustomerService.Create(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerModel model)
        {
            m_CustomerService.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            m_CustomerService.Delete(id);
            return Ok();
        }
    }
}
