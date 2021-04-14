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

        public GameService(IApplicationUserService applicationUserService, ApplicationDbContext context)
        {
            _context = context;
            this.applicationUserService = applicationUserService;
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
                .First();
        }
        
        public Game GetGameWithNextQuestionsByJoinId(string joinId)
        {
            return _context.Games.Where(g => g.JoinId == joinId)
                .Include(game => game.CurrentQuestion)
                .Include(game => game.Quiz.Questions.Where(question => question.Position > game.CurrentQuestion.Position).OrderBy(question => question.Position).Take(1))
                .First();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}