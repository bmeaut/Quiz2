using System.Linq;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class GameService: IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService applicationUserService;
        private readonly IQuestionService questionService;

        public GameService(IApplicationUserService applicationUserService, ApplicationDbContext context, IQuestionService questionService)
        {
            _context = context;
            this.applicationUserService = applicationUserService;
            this.questionService = questionService;
        }

        public Game GetGameByJoinId(string joinId)
        {
            return _context.Games.Where(g => g.JoinId == joinId)
                .Include(g => g.Owner)
                .FirstOrDefault();
        }
        
        public Game GetGameWithQuestionsByJoinId(string joinId)
        {
            return _context.Games.Where(g => g.JoinId == joinId)
                .Include(game => game.CurrentQuestion)
                .Include(game => game.Quiz.Questions.OrderBy(question => question.Position))
                .ThenInclude(question => question.Answers)
                .FirstOrDefault();
        }
        
        public Game GetGameByJoinIdWithCurrentQuestion(string joinId)
        {
            return _context.Games.Where(g => g.JoinId == joinId)
                .Include(game => game.CurrentQuestion)
                .ThenInclude(question => question.Answers)
                .FirstOrDefault();
        }

        public void SetNextQuestion(Game game)
        {
            var question = questionService.GetNextQuestion(game.QuizId, game.Id);
            _context.SaveChanges();
           
            
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}