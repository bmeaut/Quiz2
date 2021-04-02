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
            return _context.Games.First(g => g.JoinId == joinId);
        }
        
        public Game GetGameWithQuestionsByJoinId(string joinId)
        {
            return _context.Games.Where(g => g.JoinId == joinId)
                .Include(game => game.Quiz.Questions.OrderBy(question => question.Position))
                //.ThenInclude(quiz => quiz.Questions.OrderBy(question => question.Position))
                .First();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}