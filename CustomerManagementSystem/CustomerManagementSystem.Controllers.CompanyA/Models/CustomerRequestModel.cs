using System;

namespace CustomerManagementSystem.Controllers.CompanyA.Models
{
    public class CustomerRequestModel
    {
        public string   TCKN      { get; set;}
        public string   Name      { get; set;}
        public string   Surname   { get; set;}
        public DateTime BirthDate { get; set;}
    }
}
