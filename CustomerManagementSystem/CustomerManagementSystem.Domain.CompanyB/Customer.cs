namespace CustomerManagementSystem.Domain.CompanyB
{
    public class Customer : BaseEntity
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    GSM       { get; set; }
        public double    Score     { get; set;}
        public Education Education { get; set; }
    }
}
