using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.Helper;
using Quiz2.Models;

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
                .Where(userAnswer => userAnswer.GameId == gameId)
                .Count(userAnswer => userAnswer.AnswerId == answerId);
        }

        public List<Game> GetOwnedGameHistory(string ownerId)
        {
            return _context.Games
                .Include(game => game.Owner)
                .Include(game => game.Quiz)
                .Include(game => game.JoinedUsers)
                .Where(game => game.Owner.Id == ownerId)
                .ToList();
        }
        public int GetUserPointsInGame(int gameId, string userId)
        {
            return _context.UserAnswers
                .Where(userAnswer => userAnswer.GameId == gameId && userAnswer.ApplicationUserId == userId)
                .Include(userAnswer => userAnswer.Answer)
                .ThenInclude(answer => answer.Question)
                .Where(userAnswer => userAnswer.Answer.Correct)
                .Sum(userAnswer => userAnswer.Answer.Question.Points);
        }

        public List<Game> GetPlayedGameHistory(string userId)
        {
            return _context.Games
                .Include(game => game.Owner)
                .Include(game => game.Quiz)
                .Include(game => game.JoinedUsers)
                .Where(game => game.Owner.Id != userId)
                .ToList();

            ///???
            /*
            return (List<Game>)_context.ApplicationUsers
                .Find(userId)
                .Games;
            */
        }

    }
}