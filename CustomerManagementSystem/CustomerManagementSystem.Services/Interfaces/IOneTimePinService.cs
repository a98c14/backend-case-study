namespace CustomerManagementSystem.Services.Interfaces
{
    public interface IOneTimePinService
    {
        public string GenerateToken();
        public void SendOtpMessage(string gsmNumber, string token);
    }
}
