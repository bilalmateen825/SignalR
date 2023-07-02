using Microsoft.AspNetCore.SignalR;

namespace Broadcaster.Hubs
{
    public class CounterHub : Hub
    {
        public Task AddToTotal(string stUser, int value)
        {
            return Clients.All.SendAsync("CounterIncrement", stUser, value);
        }
    }
}
