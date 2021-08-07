using CustomerManagementSystem.Domain.CompanyC;

namespace CustomerManagementSystem.Controllers.CompanyC.Models
{
    public class CustomerRequestModel
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    Email     { get; set; }
        public Education Education { get; set; }
    }
}
