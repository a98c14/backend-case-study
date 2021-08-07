using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Core.Live.Hubs
{
    class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
