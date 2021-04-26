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
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class GameHub : Hub {
        private readonly IGameService gameService;
        private readonly IAnswerService answerService;
        private readonly IApplicationUserService applicationUserService;
        private readonly IUserAnswerService userAnswerService;

        public GameHub(IGameService gameService, IAnswerService answerService, IApplicationUserService applicationUserService, IUserAnswerService userAnswerService)
        {
            this.gameService = gameService;
            this.answerService = answerService;
            this.applicationUserService = applicationUserService;
            this.userAnswerService = userAnswerService;
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
            var game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
            if (game != null)
            {
                Console.WriteLine(game.CurrentQuestion.Id);
                await Clients.All.SendAsync("newQuestion", game.CurrentQuestion);
                gameService.SetNextQuestion(game);
            }
        }

        public void SendAnswer(string joinId, int answerId)
        {
            userAnswerService.CreateUserAnswer(joinId, answerId, Context.UserIdentifier);
        }
        
        public void CreateGame(int quizId)
        {
            var game = gameService.CreateGame(quizId, Context.UserIdentifier);
            Groups.AddToGroupAsync(Context.ConnectionId, game.JoinId+"Owner");
            Clients.Caller.SendAsync("ownerJoined", game.JoinId);
        }

    }
}