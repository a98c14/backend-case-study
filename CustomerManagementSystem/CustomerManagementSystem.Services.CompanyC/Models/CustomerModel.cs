using CustomerManagementSystem.Domain.CompanyC;

namespace CustomerManagementSystem.Services.CompanyC.Models
{
    public class CustomerModel
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    Email     { get; set; }
        public Education Education { get; set; }
    }
}
