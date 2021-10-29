using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Extensions;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGameService _gameService;
        private readonly IAnswerService _answerService;
        private readonly IApplicationUserService _applicationUserService;

        public UserAnswerService(
            ApplicationDbContext context, 
            IGameService gameService, 
            IAnswerService answerService,
            IApplicationUserService applicationUserService
            ) 
        {
            _context = context;
            _gameService = gameService;
            _answerService = answerService;
            _applicationUserService = applicationUserService;
        }

        public UserAnswer CreateUserAnswer(string joinId, int answerId, string applicationUserId, double timeOfSubmit)
        {
            var game = _gameService.GetGameByJoinId(joinId);
            var answer = _answerService.GetAnswer(answerId);
            var user = _applicationUserService.GetUser(applicationUserId);
            if (game == null || answer == null || user == null)
            {
                return null;
            }
            var userAnswer = new UserAnswer()
            {
                Game = game,
                ApplicationUser = user,
                Answer = answer,
                TimeOfSubmit = timeOfSubmit
            };
            _context.UserAnswers.Add(userAnswer);
            _context.SaveChanges();
            return userAnswer;
        }

        public List<int> GetCurrentQuestionStat(string joinId, ApplicationDbContext applicationDbContext = null)
        {
            Game game = null;
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
                game = _gameService.GetGameByJoinIdWithCurrentQuestion(joinId);
            }
            else
            {
                game = _gameService.GetGameByJoinIdWithCurrentQuestion(joinId, applicationDbContext);
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
                game = _gameService.GetGameByJoinId(joinId);
            }
            else
            {
                game = _gameService.GetGameByJoinId(joinId, applicationDbContext);
            }
            return applicationDbContext.UserAnswers
                .Where(userAnswer => userAnswer.GameId == game.Id)
                .Where(userAnswer => userAnswer.ApplicationUserId == applicationUserId)
                .Include(userAnswer => userAnswer.Answer)
                .Include(userAnswer => userAnswer.Answer.Question)
                .Sum(userAnswer => userAnswer.Answer.Correct ? userAnswer.Answer.Question.Points : -userAnswer.Answer.Question.Points);
        }

        public bool IsMarked(int answerId, int gameId, string applicationUserId, ApplicationDbContext applicationDbContext = null)
        {
            applicationDbContext ??= _context;

            return applicationDbContext.UserAnswers
                .Where(userAnswer => userAnswer.GameId == gameId)
                .Where(userAnswer => userAnswer.ApplicationUserId == applicationUserId)
                .Any(userAnswer => userAnswer.AnswerId == answerId);
        }

        public bool IsTheFirstAnswer(int gameId, int? questionId, string applicationUserId)
        {
            return _context.UserAnswers
                .Where(userAnswer => userAnswer.GameId == gameId)
                .Where(userAnswer => userAnswer.Answer.QuestionId == questionId)
                .Where(userAnswer => userAnswer.ApplicationUserId == applicationUserId)
                .IsNullOrEmpty();
        }
    }
}