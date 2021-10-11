using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Helper;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Hubs
{
    public delegate void Notify(string joinId);
    
    [Authorize]
    public class GameHub : Hub {
        private readonly IGameService gameService;
        private readonly IAnswerService answerService;
        private readonly IApplicationUserService applicationUserService;
        private readonly IUserAnswerService userAnswerService;
        private readonly IServiceScopeFactory serviceScopeFactory;
        public  QuestionTimer QuestionTimer = new QuestionTimer(1000);
     public GameHub(
         IGameService gameService, 
         IAnswerService answerService, 
         IApplicationUserService applicationUserService, 
         IUserAnswerService userAnswerService,
         IServiceScopeFactory serviceScopeFactory
     )
        {
            this.gameService = gameService;
            this.answerService = answerService;
            this.applicationUserService = applicationUserService;
            this.userAnswerService = userAnswerService;
            this.serviceScopeFactory = serviceScopeFactory;
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
                QuestionTimer questionTimer = new QuestionTimer(game.CurrentQuestion.SecondsToAnswer * 1000);
             //   QuestionTimer = QuestionTimer.Timers.GetOrAdd(joinId, QuestionTimer);
                 questionTimer.callerContext = Context;
                 questionTimer.hubCallerClients = Clients;
                 questionTimer.AutoReset = false;
                 questionTimer.Elapsed +=  (sender, e) => EndingQuestion(sender, game);
                 questionTimer.Interval = game.CurrentQuestion.SecondsToAnswer * 1000;
                 questionTimer.Enabled = true;
          
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
                    QuestionTimer questionTimer = new QuestionTimer(game.CurrentQuestion.SecondsToAnswer * 1000);
                    //QuestionTimer = QuestionTimer.Timers.GetOrAdd(joinId, QuestionTimer);
                    questionTimer.callerContext = Context;
                    questionTimer.hubCallerClients = Clients;
                    questionTimer.AutoReset = false;
                    questionTimer.Elapsed +=  (sender, e) => EndingQuestion(sender, game);
                    questionTimer.Interval = game.CurrentQuestion.SecondsToAnswer * 1000;
                    questionTimer.Enabled = true;
                    //task.Wait();
                }
                else
                {
                    Clients.Group(game.JoinId + "Owner").SendAsync("endGame");
                    Clients.Group(game.JoinId).SendAsync("endGame");
                }
            }
        }

        public void Proba(Game game)
        {
            Console.WriteLine("A PRÓBA LEFUTOTT  "+ game.JoinId);

        }

        public void SendAnswer(string joinId, SendAnswerDto answers)
        {
         Console.WriteLine(answers.Ids.Count);
            foreach (var answerId in answers.Ids)
            {
                userAnswerService.CreateUserAnswer(joinId, answerId, Context.UserIdentifier);
            }
            var stat = new CurrentQuestionStatDto()
            {
                Stats =  userAnswerService.getCurrentQuestionStat(joinId),
              
            };
            Clients.Group(joinId + "Owner").SendAsync("currentQuestionStat", stat);
        }
        
        public void CreateGame(int quizId)
        {
            var game = gameService.CreateGame(quizId, Context.UserIdentifier);
            Groups.AddToGroupAsync(Context.ConnectionId, game.JoinId+"Owner");
            Clients.Caller.SendAsync("ownerJoined", game.JoinId);
        }

        public void EndingQuestion(object sender, Game game)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                
                var QuestionTimer = (QuestionTimer) sender;
                Console.WriteLine("cast után " + game.JoinId);
                List<int> stats = null;
                try
                {
                    stats = userAnswerService.getCurrentQuestionStat(game.JoinId, dbContext);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
                Console.WriteLine("stat után ");
                foreach (var user in game.JoinedUsers)
                {
                    Console.WriteLine("foreach ben ");
                    var currentQuestionStatDto = new CurrentQuestionStatDto()
                    {
                        Stats = stats,
                        Point = userAnswerService.getUserPoint(game.JoinId, user.Id, dbContext)
                    };
                     QuestionTimer.hubCallerClients.User(user.Id).SendAsync("endQuestion", currentQuestionStatDto);
                }

                QuestionTimer.hubCallerClients.Group(game.JoinId + "Owner").SendAsync("endQuestionOwner");
                Console.WriteLine("függvény végén");

            }
        }
    }
}