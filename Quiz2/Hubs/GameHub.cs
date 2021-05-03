using System;
using System.Linq;
using System.Net;
using System.Threading;
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
                    Clients.Caller.SendAsync("joined", joinId);
                    gameService.AddJoinedUser(game.Id, Context.UserIdentifier);
                    var names = gameService.GetJoinedUsersNames(game.Id);
                    Clients.Group(game.JoinId+"Owner").SendAsync("newPlayer",  names);
                    Clients.Group(game.JoinId).SendAsync("newPlayer",  names);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Clients.Caller.SendAsync("joinFailed");
            }

            
        }

        public void StartGame(string joinId)
        {
            try
            {
                var game = gameService.GetGameWithQuestionsByJoinId(joinId);
                game.CurrentQuestion = game.Quiz.Questions[0];
                game.Status = GameStatuses.Started;
                gameService.Save();
                Clients.Group(game.JoinId+"Owner").SendAsync("startedOwner",  game.CurrentQuestion);
                Clients.Group(game.JoinId).SendAsync("started",  game.CurrentQuestion);
                EndingQuestion(game);
                //Task.Run(() => EndingQuestion(game) );
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Clients.Caller.SendAsync("startFailed");
            }
        }

        public void NextQuestion(string joinId)
        {
            Console.WriteLine(joinId);
            var game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
            if (game != null)
            {
                Console.WriteLine(game.CurrentQuestion.Id);
                gameService.SetNextQuestion(game);
                if (game.CurrentQuestion != null)
                {
                    Clients.Group(game.JoinId + "Owner").SendAsync("newQuestion", game.CurrentQuestion);
                    Clients.Group(game.JoinId).SendAsync("newQuestion", game.CurrentQuestion);
                    EndingQuestion(game);
                    //Task.Run(() => EndingQuestion(game) );
                }
                else
                {
                    Clients.Group(game.JoinId + "Owner").SendAsync("endGame");
                    Clients.Group(game.JoinId).SendAsync("endGame");
                }
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

        private void EndingQuestion(Game game)
        {
            Console.WriteLine("slepp előtt");
            Task.Delay(game.CurrentQuestion.SecondsToAnswer * 1000).Wait();
            Console.WriteLine("slepp után");
            Clients.Group(game.JoinId+"Owner").SendAsync("endQuestion");
            Clients.Group(game.JoinId).SendAsync("endQuestion");
            Console.WriteLine("NextQuestion előtt");
            NextQuestion(game.JoinId);
            Console.WriteLine("függvény végén");
        }
    }
}