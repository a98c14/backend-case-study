using CustomerManagementSystem.Controllers.CompanyA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerManagementSystem.Controllers.CompanyA
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {     
        private readonly ILogger<CustomersController> m_Logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            m_Logger = logger;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CustomerRequestModel model)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerRequestModel model)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
