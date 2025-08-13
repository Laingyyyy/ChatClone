using Microsoft.AspNetCore.SignalR;

namespace Server
{
    public class ChatHub : Hub
    {
        public Task<string> Ping(string payload) => Task.FromResult($"Pong: {payload}");
    }
}