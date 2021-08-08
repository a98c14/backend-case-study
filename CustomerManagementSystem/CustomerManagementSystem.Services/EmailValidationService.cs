using CustomerManagementSystem.Services.Interfaces;

namespace CustomerManagementSystem.Services
{
    public class EmailValidationService : IEmailValidationService
    {
        public long GenerateToken()
        {
            // TODO(selim): Generate a token 
            return 0;
        }

        public void SendOTPMail(string mail, string token)
        {
            // TODO(selim): Send mail to user with given token
        }
    }
}
