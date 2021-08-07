using CustomerManagementSystem.Domain.CompanyB;

namespace CustomerManagementSystem.Services.CompanyB.Models
{
    public class CustomerModel
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    GSM       { get; set; }
        public Education Education { get; set; }

        public CustomerModel()
        {

        }

        public CustomerModel(Customer c)
        {
            Name = c.Name;
            Surname = c.Surname;
            GSM = c.GSM;
            Education = c.Education;
        }
    }
}
