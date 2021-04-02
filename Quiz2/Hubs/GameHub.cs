using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Quiz2.Data;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Hubs
{
    public class GameHub : Hub {
        private readonly IGameService gameService;

        public GameHub(IGameService gameService)
        {

            this.gameService = gameService;
        }
        public async Task GroupTest() {
            await Clients.Groups("group1").SendAsync("groupTestAnswer","gruop1 jóóóó");
            await Clients.Group("group2").SendAsync("groupTestAnswer","gruop2 végre");

        }
        public void JoinGame(string joinId)
        {
            try
            {
                Console.WriteLine(Context.User.Identity.Name);
                var game = gameService.GetGameByJoinId(joinId);
                Groups.AddToGroupAsync(Context.ConnectionId, joinId);
                Clients.Caller.SendAsync("joined");
            }
            catch(Exception e)
            {
                Clients.Caller.SendAsync("joinFailed");
            }

            
        }

        public void StartGame(string joinId)
        {
            try
            {
                var game = gameService.GetGameWithQuestionsByJoinId(joinId);
                game.CurrentQuestion = game.Quiz.Questions[0];
                gameService.Save();
                Clients.Caller.SendAsync("started");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Clients.Caller.SendAsync("startFailed");
            }
        }

    }
}