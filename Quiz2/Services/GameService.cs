using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class GameService: IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService applicationUserService;
        private readonly IQuestionService questionService;
        private readonly IQuizService quizService;


        public GameService(
            IApplicationUserService applicationUserService, 
            ApplicationDbContext context, 
            IQuestionService questionService, 
            IQuizService quizService
        )
        {
            _context = context;
            this.applicationUserService = applicationUserService;
            this.questionService = questionService;
            this.quizService = quizService;
        }

        public Game GetGameByJoinId(string joinId, ApplicationDbContext applicationDbContext = null)
        {
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
            }
            return applicationDbContext.Games.Where(g => g.JoinId == joinId)
                    .Include(g => g.Owner)
                    .FirstOrDefault();
            
        }
        
        public Game GetGameWithQuestionsByJoinId(string joinId)
        {
            return _context.Games.Where(g => g.JoinId == joinId)
                .Include(game => game.CurrentQuestion)
                .Include(game => game.JoinedUsers)
                .Include(game => game.Quiz.Questions.OrderBy(question => question.Position))
                .ThenInclude(question => question.Answers)
                .FirstOrDefault();
        }
        
        public Game GetGameByJoinIdWithCurrentQuestion(string joinId, ApplicationDbContext applicationDbContext = null)
        {
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
            }
   
            return applicationDbContext.Games.Where(g => g.JoinId == joinId)
                .Include(game => game.CurrentQuestion)
                .ThenInclude(question => question.Answers.OrderBy(answer => answer.Id))
                .FirstOrDefault();
        }
        
        public Game GetGameByJoinIdWithCurrentQuestionAndJoinUsers(string joinId, ApplicationDbContext applicationDbContext = null)
        {
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
            }
   
            return applicationDbContext.Games.Where(g => g.JoinId == joinId)
                .Include(game => game.CurrentQuestion)
                .ThenInclude(question => question.Answers.OrderBy(answer => answer.Id))
                .Include(game => game.JoinedUsers)
                .FirstOrDefault();
        }

        public void SetNextQuestion(Game game)
        {
            var question = questionService.GetNextQuestion(game.QuizId, game.CurrentQuestion.Position);
            if (question != null)
            {
                Console.WriteLine("question != null");
                game.CurrentQuestion = question;
                game.CurrentQuestionStarted = DateTime.Now;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("question == null");
                game.CurrentQuestion = null;
                game.Status = GameStatuses.Finished;
                _context.SaveChanges();
            }

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Game CreateGame(int quizId, string ownerId)
        {
            var question = quizService.GetFirstQuestion(quizId);
            var game = new Game()
            {
                QuizId = quizId,
                OwnerId = ownerId,
                JoinId = GenerateJoinId(),
                Status = GameStatuses.Created,
                CurrentQuestion = question,
                CurrentQuestionStarted = DateTime.MinValue
            };
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }

        private string GenerateJoinId()
        {
            string joinId;
            do {
                joinId = Path.GetRandomFileName();
                joinId = joinId.Substring(0, 8);
            } while (GetGameByJoinId(joinId) != null);

            return joinId;
        }

        public void AddJoinedUser(int gameId, string applicationUserId)
        {
            var applicationUser =applicationUserService.GetUser(applicationUserId);
            var game = _context.Games.Where(g => g.Id == gameId)
                .Include(g => g.JoinedUsers)
                .FirstOrDefault();
            if (applicationUser != null && game != null)
            {
                game.JoinedUsers.Add(applicationUser);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Nincs ilyen user!");
            }
        }

        public IEnumerable<PlayerDto> GetJoinedUsersNames(int gameId)
        {
            var users = _context.Games.Where(game => game.Id == gameId)
                .Include(game => game.JoinedUsers)
                .Select(game1 => game1.JoinedUsers)
                .FirstOrDefault();
            
            var names = from user in users
                select new PlayerDto()
                {
                    Id = user.Id,
                    Name = user.UserName,
                };
            return names;
        }

    }
}