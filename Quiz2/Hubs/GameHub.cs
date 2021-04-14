using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Quiz2.Data;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Hubs
{
    [Authorize]
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
                Console.WriteLine(Context.UserIdentifier);
                var game = gameService.GetGameByJoinId(joinId);
                if (game.Owner.Id == Context.UserIdentifier)
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, joinId+"Owner");
                    Clients.Caller.SendAsync("ownerJoined");
                }
                else
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, joinId);
                    Clients.Caller.SendAsync("joined");
                }
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
                Clients.Group(game.JoinId+"Owner").SendAsync("startedOwner",  game.CurrentQuestion);
                Clients.Group(game.JoinId).SendAsync("started",  game.CurrentQuestion);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Clients.Caller.SendAsync("startFailed");
            }
        }

        public async void NextQuestion(string joinId)
        {
            Console.WriteLine(joinId);
            var game = gameService.GetGameWithQuestionsByJoinId(joinId);
            Console.WriteLine(game.CurrentQuestion.Id);
            //await Clients.Groups(joinId).SendAsync("newQuestion",game.CurrentQuestion);
            await Clients.All.SendAsync("newQuestion", game.CurrentQuestion);
            game.CurrentQuestion = game.Quiz.Questions[1];
            gameService.Save();
        }

    }
}