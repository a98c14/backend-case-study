using CustomerManagementSystem.Multitenancy;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        [Route("Test")]
        public IActionResult Test() =>
            Ok("Its ok :)");

        [HttpGet]
        [Route("Tenant")]
        public string GetTenant() => 
            HttpContext.GetTenant().Id;
    }
}
