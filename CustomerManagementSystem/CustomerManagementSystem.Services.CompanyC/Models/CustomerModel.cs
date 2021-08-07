using CustomerManagementSystem.Domain.CompanyC;

namespace CustomerManagementSystem.Services.CompanyC.Models
{
    public class CustomerModel
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    Email     { get; set; }
        public Education Education { get; set; }

        public CustomerModel()
        {

        }

        public CustomerModel(Customer c)
        {
            Name = c.Name;
            Surname = c.Surname;
            Email = c.Email;
            Education = c.Education;
        }
    }
}
