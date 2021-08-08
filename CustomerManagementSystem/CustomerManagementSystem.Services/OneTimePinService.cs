using CustomerManagementSystem.Services.Interfaces;

namespace CustomerManagementSystem.Services
{
    public class OneTimePinService : IOneTimePinService
    {
        public string GenerateToken()
        {
            // TODO(selim): Generate a token 
            return "token";
        }

        public void SendOtpMessage(string gsmNumber, string token)
        {
            // TODO(selim): Send message to user with given token
        }
    }
}
