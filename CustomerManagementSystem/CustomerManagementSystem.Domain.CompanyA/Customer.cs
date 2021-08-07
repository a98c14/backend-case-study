using System;

namespace CustomerManagementSystem.Domain.CompanyA
{
    public class Customer : BaseEntity
    {
        public string   TCKN      { get; set; }
        public string   Name      { get; set; }
        public string   Surname   { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
