namespace CustomerManagementSystem.Services.Interfaces
{
    public interface IEmailValidationService
    {
        public long GenerateToken();
        public void SendOTPMail(string mail, string token);
    }
}
