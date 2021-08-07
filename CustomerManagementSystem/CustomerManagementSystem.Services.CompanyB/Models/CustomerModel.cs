using CustomerManagementSystem.Domain.CompanyB;

namespace CustomerManagementSystem.Services.CompanyB.Models
{
    public class CustomerModel
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    GSM       { get; set; }
        public Education Education { get; set; }
    }
}
