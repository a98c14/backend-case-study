using System;

namespace CustomerManagementSystem.Services.CompanyA.Models
{
    public class CustomerModel
    {
        public string   TCKN      { get; set; }
        public string   Name      { get; set; }
        public string   Surname   { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
