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
        private static  IGameService gameService;
        private readonly IAnswerService answerService;
        private readonly IApplicationUserService applicationUserService;
        private readonly IUserAnswerService userAnswerService;
        public static IHubContext<GameHub> GlobalContext { get; private set; }
        public GameHub(IHubContext<GameHub> ctx, IGameService gameServic, IAnswerService answerService, IApplicationUserService applicationUserService, IUserAnswerService userAnswerService)
        {
            GlobalContext = ctx;
            gameService = gameServic;
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
                /*var timer = new System.Timers.Timer(game.CurrentQuestion.SecondsToAnswer * 1000);
                timer.Elapsed +=  ( sender, e ) => EndingQuestion(game);
                timer.AutoReset = false;
                timer.Start();*/
                //EndingQuestion(game);
                Task task = Task.Run(() => EndingQuestion(game) );
                task.Wait();
                NextQuestion(game.JoinId);
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                Clients.Caller.SendAsync("startFailed");
            }
        }

        public async Task NextQuestion(string joinId)
        {
            Console.WriteLine(joinId);
            Console.WriteLine(gameService.ToString());
            var game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
            Console.WriteLine("GetGameByJoinIdWithCurrentQuestion után" );
            Console.WriteLine(game?.Id );
            if (game != null)
            {
                Console.WriteLine(game.CurrentQuestion.Id);
                gameService.SetNextQuestion(game);
                Console.WriteLine("SetNextQuestion után" );
                if (game.CurrentQuestion != null)
                {
                    await GlobalContext.Clients.Group(game.JoinId + "Owner").SendAsync("newQuestion", game.CurrentQuestion);
                    await GlobalContext.Clients.Group(game.JoinId).SendAsync("newQuestion", game.CurrentQuestion);
                    /*var timer = new System.Timers.Timer(game.CurrentQuestion.SecondsToAnswer * 1000);
                    timer.AutoReset = false;
                    timer.Elapsed +=  ( sender, e ) => EndingQuestion(game);
                    timer.Start();*/
                    //EndingQuestion(game);
                    Task task = Task.Run(() => EndingQuestion(game) );
                    task.Wait();
                    NextQuestion(game.JoinId);
                }
                else
                {
                    await GlobalContext.Clients.Group(game.JoinId + "Owner").SendAsync("endGame");
                    await GlobalContext.Clients.Group(game.JoinId).SendAsync("endGame");
                }
                Console.WriteLine("NextQuestion vége");
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
            //Console.WriteLine("slepp előtt");
            Task.Delay(game.CurrentQuestion.SecondsToAnswer * 1000).Wait();
            Console.WriteLine("slepp után");
           // var hub = GetHubContext();
           GlobalContext.Clients.Group(game.JoinId+"Owner").SendAsync("endQuestion");
           GlobalContext.Clients.Group(game.JoinId).SendAsync("endQuestion");
           
           
        }
    }
}