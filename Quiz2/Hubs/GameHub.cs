using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Quiz2.Hubs
{
  
    public class GameHub : Hub {
        public async Task GroupTest() {
            await Clients.Group("group1").SendAsync("groupTestAnswer","gruop1 jóóóó");
            await Clients.Group("group2").SendAsync("groupTestAnswer","gruop2 végre");
        }
        public Task JoinGame(string joinId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, joinId);
        }

    }
}