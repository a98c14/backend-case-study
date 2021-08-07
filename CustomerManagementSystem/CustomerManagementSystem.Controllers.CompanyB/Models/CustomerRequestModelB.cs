using CustomerManagementSystem.Domain.CompanyB;

namespace CustomerManagementSystem.Controllers.CompanyB.Models
{
    public class CustomerRequestModel
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    GSM       { get; set; }
        public Education Education { get; set; }
    }
}
