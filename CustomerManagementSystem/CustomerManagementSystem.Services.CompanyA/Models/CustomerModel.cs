using CustomerManagementSystem.Domain.CompanyA;
using System;

namespace CustomerManagementSystem.Services.CompanyA.Models
{
    public class CustomerModel
    {
        public int      Id        { get; set; }
        public string   TCKN      { get; set; }
        public string   Name      { get; set; }
        public string   Surname   { get; set; }
        public DateTime BirthDate { get; set; }

        public CustomerModel()
        {

        }

        public CustomerModel(Customer c)
        {
            Id = c.Id;
            TCKN = c.TCKN;
            Name = c.Name;
            Surname = c.Surname;
            BirthDate = c.BirthDate;
        }
    }
}
