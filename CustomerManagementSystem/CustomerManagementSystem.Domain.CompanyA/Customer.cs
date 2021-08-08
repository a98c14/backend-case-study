using System;

namespace CustomerManagementSystem.Domain.CompanyA
{
    public class Customer : BaseEntity
    {
        public long     TCKN      { get; set; }
        public string   Name      { get; set; }
        public string   Surname   { get; set; }
        public double   Score     { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
