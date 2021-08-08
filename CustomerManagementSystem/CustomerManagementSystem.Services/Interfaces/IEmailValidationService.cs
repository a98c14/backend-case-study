namespace CustomerManagementSystem.Services.Interfaces
{
    public interface IEmailValidationService
    {
        public string GenerateToken();
        public void SendOTPMail(string mail, string token);
    }
}
