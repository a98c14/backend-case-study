using CustomerManagementSystem.Domain.CompanyB;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Services.CompanyB.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
                      
        [Required]    
        public string Name { get; set; }
                      
        [Required]    
        public string Surname { get; set; }
                      
        [Required]    
        public string GSM { get; set; }

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
            GSM = c.GSM;
            Education = c.Education;
        }
    }
}
