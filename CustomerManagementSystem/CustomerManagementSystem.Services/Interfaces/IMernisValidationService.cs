namespace CustomerManagementSystem.Services.Interfaces
{
    public interface IMernisValidationService
    {
        bool ValidateCustomer(long tckn, string name, string surname, int birthdate);
    }
}
