namespace CustomerManagementSystem.Domain.CompanyC
{
    public class Customer : BaseEntity
    {
        public string    Name      { get; set; }
        public string    Surname   { get; set; }
        public string    Email     { get; set; }
        public Education Education { get; set; }
        public double    Score     { get; set;}
    }
}
