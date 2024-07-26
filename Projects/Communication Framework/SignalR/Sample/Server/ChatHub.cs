using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Server
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> _connections = new Dictionary<string, string>();

        public async Task JoinRoom(string name)
        {
            _connections[Context.ConnectionId] = name;
            await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", "System", $"{name} joined the room");
            await Clients.All.SendAsync("ReceiveMessage", "System", $"{_connections.Count} users connected");
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out var name))
            {
                _connections.Remove(Context.ConnectionId);
                await Clients.All.SendAsync("ReceiveMessage", "System", $"{name} left the room");
                await Clients.All.SendAsync("ReceiveMessage", "System", $"{_connections.Count} users connected");
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}