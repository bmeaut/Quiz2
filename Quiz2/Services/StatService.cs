using System.Linq;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;

namespace Quiz2.Services
{
    public class StatService: IStatService
    {
        
        private readonly ApplicationDbContext _context;

        public StatService(ApplicationDbContext context)
        {
            _context = context;
        }
        public int GetNumberOfUserAnswers(int gameId, int answerId)
        {
            return _context.UserAnswers
                .Include(userAnswer => userAnswer.Game)
                .Include(userAnswer => userAnswer.Answer)
                .Where(userAnswer => userAnswer.Game.Id == gameId)
                .Count(userAnswer => userAnswer.Answer.Id == answerId);
        }
    }
}