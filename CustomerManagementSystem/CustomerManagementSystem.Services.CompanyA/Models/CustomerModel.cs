using CustomerManagementSystem.Domain.CompanyA;
using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerManagementSystem.Services.CompanyA.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        [Required]
        public long TCKN { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime? Birthdate { get; set; }

        public CustomerModel()
        {

        }

        public CustomerModel(Customer c)
        {
            Id = c.Id;
            TCKN = c.TCKN;
            Name = c.Name;
            Surname = c.Surname;
            Birthdate = c.Birthdate;
        }
    }
}
