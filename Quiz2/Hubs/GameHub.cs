using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Quiz2.Data;
using Quiz2.DTO;
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
                game.Status = GameStatuses.Started;
                game.CurrentQuestionStarted = DateTime.Now;
                gameService.Save();
                Clients.Group(game.JoinId+"Owner").SendAsync("startedOwner",  game.CurrentQuestion);
                Clients.Group(game.JoinId).SendAsync("started",  game.CurrentQuestion);
                //EndingQuestion(game);
                Task task = Task.Run(() => EndingQuestion(game) );
                task.Wait(); 
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
                    Clients.Group(game.JoinId + "Owner").SendAsync("newQuestionOwner", game.CurrentQuestion);
                    Clients.Group(game.JoinId).SendAsync("newQuestion", game.CurrentQuestion);
                    //EndingQuestion(game);
                    Task task = Task.Run(() => EndingQuestion(game) );
                    task.Wait();
                }
                else
                {
                    Clients.Group(game.JoinId + "Owner").SendAsync("endGame");
                    Clients.Group(game.JoinId).SendAsync("endGame");
                }
            }
        }

        public void SendAnswer(string joinId, SendAnswerDto answers)
        {
         Console.WriteLine(answers.Ids.Count);
            foreach (var answerId in answers.Ids)
            {
                userAnswerService.CreateUserAnswer(joinId, answerId, Context.UserIdentifier);
            }
            var stats = userAnswerService.getCurrentQuestionStat(joinId);
            Clients.Group(joinId + "Owner").SendAsync("currentQuestionStat", stats);
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
            var stats = userAnswerService.getCurrentQuestionStat(game.JoinId);
           /* PlayerStatisticDto playerStatisticDto = new PlayerStatisticDto()
            {Id = game.CurrentQuestion.Id,
                Text = game.CurrentQuestion.Text,
                Points = game.CurrentQuestion.Points,
                Stats = stats.stats,
                
                
            };
            playerStatisticDto.Stats = stats.stats;*/
           foreach (var user in  game.JoinedUsers)
           {
               Clients.User(user.Id).SendAsync("endQuestion", stats);
           }
            Clients.Group(game.JoinId+"Owner").SendAsync("endQuestionOwner");
        //    Clients.Group(game.JoinId).SendAsync("endQuestion", stats);
            Console.WriteLine("NextQuestion előtt");
            //NextQuestion(game.JoinId);
            Console.WriteLine("függvény végén");
        }
    }
}