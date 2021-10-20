using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGameService gameService;
        private readonly IAnswerService answerService;
        private readonly IApplicationUserService applicationUserService;

        public UserAnswerService(
            ApplicationDbContext context, 
            IGameService gameService, 
            IAnswerService answerService,
            IApplicationUserService applicationUserService
            ) 
        {
            _context = context;
            this.gameService = gameService;
            this.answerService = answerService;
            this.applicationUserService = applicationUserService;
        }

        public UserAnswer CreateUserAnswer(string joinId, int answerId, string applicationUserId, double timeOfSubmit)
        {
            Console.WriteLine("joinId: " + joinId);
            Console.WriteLine("answerId: " + answerId);
            Console.WriteLine("applicationUserId: " + applicationUserId);
            var game = gameService.GetGameByJoinId(joinId);
            Console.WriteLine("game után");
            var answer = answerService.GetAnswer(answerId);
            Console.WriteLine("answer után");
            var user = applicationUserService.GetUser(applicationUserId);
            Console.WriteLine("user után");

            if (game == null || answer == null || user == null)
            {
                return null;
            }
            Console.WriteLine("if ben");
            var userAnswer = new UserAnswer()
            {
                Game = game,
                ApplicationUser = user,
                Answer = answer,
                TimeOfSubmit = timeOfSubmit
            };
            Console.WriteLine("new után");
            _context.UserAnswers.Add(userAnswer);
            Console.WriteLine("Add után");
            _context.SaveChanges();
            Console.WriteLine("SaveChanges után");
            return userAnswer;
        }

        public List<int> GetCurrentQuestionStat(string joinId, ApplicationDbContext applicationDbContext = null)
        {
            Game game = null;
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
                game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
            }
            else
            {
                game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId, applicationDbContext);
            }

            
            var stats = applicationDbContext.UserAnswers
                .Where(userAnswer => userAnswer.GameId == game.Id)
                .Include(userAnswer => userAnswer.Answer)
                .Where(userAnswer => game.CurrentQuestion.Answers.Contains(userAnswer.Answer))
                .GroupBy(userAnswer => userAnswer.AnswerId)
                .OrderBy(g => g.Key)
                .Select(g => new {AnswerId = g.Key, Count = g.Count()})
                .ToList();

            var counts = new List<int>();
            game.CurrentQuestion.Answers.Sort((x, y) => x.Id.CompareTo(y.Id));


            foreach (var answer in game.CurrentQuestion.Answers)
            {

                var find = false;
                foreach (var stat in stats)
                {
                    if (stat.AnswerId == answer.Id)
                    {
                        find = true;
                        counts.Add(stat.Count);
                    }
                }

                if (!find)
                {
                    counts.Add(0);
                }

            }
            return counts;
        }

        public int GetUserPoint(string joinId, string applicationUserId, ApplicationDbContext applicationDbContext = null)
        {
            Game game = null;
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
                game = gameService.GetGameByJoinId(joinId);
            }
            else
            {
                game = gameService.GetGameByJoinId(joinId, applicationDbContext);
            }
            return applicationDbContext.UserAnswers
                .Where(userAnswer => userAnswer.GameId == game.Id)
                .Where(userAnswer => userAnswer.ApplicationUserId == applicationUserId)
                .Include(userAnswer => userAnswer.Answer)
                .Include(userAnswer => userAnswer.Answer.Question)
                .Where(userAnswer => userAnswer.Answer.Correct)
                .Sum(userAnswer => userAnswer.Answer.Question.Points);
            
        }

        public bool IsMarked(int answerId, int gameId, string applicationUserId, ApplicationDbContext applicationDbContext = null)
        {
            if (applicationDbContext == null)
            { 
                applicationDbContext = _context; 
            }

            return applicationDbContext.UserAnswers
                .Where(userAnswer => userAnswer.GameId == gameId)
                .Where(userAnswer => userAnswer.ApplicationUserId == applicationUserId)
                .Where(userAnswer => userAnswer.AnswerId == answerId)
                .Any();
        }

    }
}