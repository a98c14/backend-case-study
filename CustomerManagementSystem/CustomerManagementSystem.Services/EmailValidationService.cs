using CustomerManagementSystem.Services.Interfaces;

namespace CustomerManagementSystem.Services
{
    public class EmailValidationService : IEmailValidationService
    {
        public string GenerateToken()
        {
            // TODO(selim): Generate a token 
            return "token";
        }

        public void SendOTPMail(string mail, string token)
        {
            // TODO(selim): Send mail to user with given token
        }
    }
}
