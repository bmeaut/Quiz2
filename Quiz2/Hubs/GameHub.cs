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
                if (game == null)
                {
                    Clients.Caller.SendAsync("gameNotExist");
                    return;
                }
                if (game.Owner.Id == Context.UserIdentifier)
                {
                    switch (game.Status)
                    {
                        case GameStatuses.Created:
                            Groups.AddToGroupAsync(Context.ConnectionId, joinId+"Owner");
                            Clients.Caller.SendAsync("ownerJoined");
                            break;
                        case GameStatuses.Started:
                            Groups.AddToGroupAsync(Context.ConnectionId, joinId+"Owner");
                            var gameWithCurrentQuestion = gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
                            var questionToSend = new SendQuestionDto()
                            {
                                Id = gameWithCurrentQuestion.CurrentQuestion.Id,
                                Text = gameWithCurrentQuestion.CurrentQuestion.Text,
                                Answers = gameWithCurrentQuestion.CurrentQuestion.Answers.Select(answer =>
                                    new SendAnswerDto()
                                    {
                                        Id = answer.Id,
                                        Text = answer.Text
                                    }).ToList(),
                                SecondsToAnswer = gameWithCurrentQuestion.CurrentQuestion.SecondsToAnswer,
                                Points = gameWithCurrentQuestion.CurrentQuestion.Points,
                            };
                            var ownerJoinedToStarted = new OwnerJoinedToStartedDto()
                                {
                                    Question =  questionToSend,
                                    CurrentQuestionStat =  new CurrentQuestionStatDto()
                                    {
                                        Stats =  userAnswerService.GetCurrentQuestionStat(joinId),
                                    },
                                    RemainingTime = gameWithCurrentQuestion.CurrentQuestion.SecondsToAnswer - (DateTime.Now - gameWithCurrentQuestion.CurrentQuestionStarted).Seconds,
                                    JoinId = gameWithCurrentQuestion.JoinId,
                                };
                            Clients.Caller.SendAsync("ownerJoinedToStarted", ownerJoinedToStarted);
                            break;
                        case GameStatuses.Finished:
                            Clients.Caller.SendAsync("gameFinished");
                            break;
                    }
                }
                else
                {
                    switch (game.Status)
                    {
                        case GameStatuses.Created:
                            Groups.AddToGroupAsync(Context.ConnectionId, joinId);
                            Clients.Caller.SendAsync("joined", joinId);
                            gameService.AddJoinedUser(game.Id, Context.UserIdentifier);
                            var names = gameService.GetJoinedUsersNames(game.Id);
                            Clients.Group(game.JoinId+"Owner").SendAsync("newPlayer",  names);
                            Clients.Group(game.JoinId).SendAsync("newPlayer",  names);
                            break;
                        case GameStatuses.Started:
                            Groups.AddToGroupAsync(Context.ConnectionId, joinId);
                            Clients.Caller.SendAsync("joinedToStarted", joinId);
                            gameService.AddJoinedUser(game.Id, Context.UserIdentifier);
                            break;
                        case GameStatuses.Finished:
                            Clients.Caller.SendAsync("gameFinished");
                            break;
                    }
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
                var game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
                game.Status = GameStatuses.Started;
                game.CurrentQuestionStarted = DateTime.Now;
                gameService.Save();

                var questionToSend = new SendQuestionDto()
                {
                    Id = game.CurrentQuestion.Id,
                    Text = game.CurrentQuestion.Text,
                    Answers = game.CurrentQuestion.Answers.Select(answer =>
                        new SendAnswerDto()
                        {
                            Id = answer.Id,
                            Text = answer.Text
                        }).ToList(),
                    SecondsToAnswer = game.CurrentQuestion.SecondsToAnswer,
                    Points = game.CurrentQuestion.Points,
                };
                Clients.Group(game.JoinId+"Owner").SendAsync("startedOwner",  questionToSend);
                Clients.Group(game.JoinId).SendAsync("started",  questionToSend);
                
                QuestionTimer questionTimer = new QuestionTimer(game.CurrentQuestion.SecondsToAnswer * 1000);
                questionTimer = QuestionTimer.Timers.GetOrAdd(joinId, questionTimer);
                 questionTimer.callerContext = Context;
                 questionTimer.hubCallerClients = Clients;
                 questionTimer.AutoReset = false;
                 questionTimer.Elapsed +=  (sender, e) => EndingQuestion(sender, game.JoinId);
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
                //Console.WriteLine(game.CurrentQuestion.Id);
                gameService.SetNextQuestion(game);
                if (game.CurrentQuestion != null)
                {
                    var questionToSend = new SendQuestionDto()
                    {
                        Id = game.CurrentQuestion.Id,
                        Text = game.CurrentQuestion.Text,
                        Answers = game.CurrentQuestion.Answers.Select(answer =>
                            new SendAnswerDto()
                            {
                                Id = answer.Id,
                                Text = answer.Text
                            }).ToList(),
                        SecondsToAnswer = game.CurrentQuestion.SecondsToAnswer,
                        Points = game.CurrentQuestion.Points,
                    };
                    Clients.Group(game.JoinId + "Owner").SendAsync("newQuestionOwner", questionToSend);
                    Clients.Group(game.JoinId).SendAsync("newQuestion", questionToSend);
                    QuestionTimer questionTimer = new QuestionTimer(game.CurrentQuestion.SecondsToAnswer * 1000);
                    questionTimer = QuestionTimer.Timers.GetOrAdd(joinId, questionTimer);
                    questionTimer.callerContext = Context;
                    questionTimer.hubCallerClients = Clients;
                    questionTimer.AutoReset = false;
                    questionTimer.Elapsed +=  (sender, e) => EndingQuestion(sender, game.JoinId);
                    questionTimer.Interval = game.CurrentQuestion.SecondsToAnswer * 1000;
                    questionTimer.Enabled = true;
                }
                else
                {
                    game.Status = GameStatuses.Finished;
                    gameService.Save();
                    Clients.Group(game.JoinId + "Owner").SendAsync("endGameOwner", game.Id);
                    Clients.Group(game.JoinId).SendAsync("endGame", game.Id);
                }
            }
        }

        public void SendAnswers(string joinId, SendAnswersDto answers)
        {
             Console.WriteLine(answers.Ids.Count);
             var game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
             var timeOfSubmit = (DateTime.Now - game.CurrentQuestionStarted).TotalSeconds;

             if (timeOfSubmit > game.CurrentQuestion.SecondsToAnswer || !userAnswerService.IsTheFirstAnswer(game.Id, game.CurrentQuestionId, Context.UserIdentifier))
             {
                 return;
             }
             
             foreach (var answerId in answers.Ids)
             {
                userAnswerService.CreateUserAnswer(joinId, answerId, Context.UserIdentifier, timeOfSubmit);
             }
             var stat = new CurrentQuestionStatDto()
             {
                 Stats =  userAnswerService.GetCurrentQuestionStat(joinId),
             };
             Clients.Group(joinId + "Owner").SendAsync("currentQuestionStat", stat);
        }
        
        public void CreateGame(int quizId)
        {
            var game = gameService.CreateGame(quizId, Context.UserIdentifier);
            Groups.AddToGroupAsync(Context.ConnectionId, game.JoinId+"Owner");
            Clients.Caller.SendAsync("ownerJoined", game.JoinId);
        }

        public void EndingQuestion(object sender, string joinId)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            var game = gameService.GetGameByJoinIdWithCurrentQuestionAndJoinUsers(joinId, dbContext);
            var QuestionTimer = (QuestionTimer) sender;
            Console.WriteLine("cast után " + game.JoinId);
            List<int> stats = userAnswerService.GetCurrentQuestionStat(game.JoinId, dbContext);
            Console.WriteLine("stat után ");
            foreach (var user in game.JoinedUsers)
            {
                Console.WriteLine("foreach ben ");
                var correctedAnswers = game.CurrentQuestion.Answers.Select(answer =>
                    new CorrectedAnswerDto()
                    {
                        Id = answer.Id,
                        Correct = answer.Correct,
                        Text = answer.Text,
                        Marked = userAnswerService.IsMarked(answer.Id, game.Id, user.Id, dbContext),
                    }
                ).ToList();
                Console.WriteLine("tolist után");
                CurrentQuestionStatDto currentQuestionStatDto = new CurrentQuestionStatDto()
                    {
                        CorrectedAnswers = correctedAnswers,
                        Stats = stats,
                        Point = userAnswerService.GetUserPoint(game.JoinId, user.Id, dbContext)
                    };
              

                Console.WriteLine("new CurrentQuestionStatDto után");
                QuestionTimer.hubCallerClients.User(user.Id).SendAsync("endQuestion", currentQuestionStatDto);
            }

            QuestionTimer.hubCallerClients.Group(game.JoinId + "Owner").SendAsync("endQuestionOwner");
            Console.WriteLine("függvény végén");
        }
    }
}