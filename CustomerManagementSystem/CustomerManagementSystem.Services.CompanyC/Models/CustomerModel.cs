using CustomerManagementSystem.Domain.CompanyC;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Services.CompanyC.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [Required]
        public Education Education { get; set; }

        public CustomerModel()
        {

        }

        public CustomerModel(Customer c)
        {
            Id = c.Id;
            Name = c.Name;
            Surname = c.Surname;
            Email = c.Email;
            Education = c.Education;
        }
    }
}
