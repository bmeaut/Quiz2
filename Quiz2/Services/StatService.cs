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
                .Include(userAnswer => userAnswer.Game)
                .Include(userAnswer => userAnswer.Answer)
                .Where(userAnswer => userAnswer.Game.Id == gameId)
                .Count(userAnswer => userAnswer.Answer.Id == answerId);
        }

        public List<Game> GetOwnedGameHistory(string ownerId)
        {
            return _context.Games
                .Include(game => game.Owner)
                .Include(game => game.Status)
                .Include(game => game.Quiz)
                .Include(game => game.JoinedUsers)
                .Where(game => game.Owner.Id == ownerId)
                .ToList();
        }
/*
        public int GetUserPointsInGame(int gameId, string userId)
        {
            return _context.Games
                .Include(game => game.JoinedUsers)
                .Where(game => game.Id == gameId && game.JoinedUsers.FirstOrDefault(user => user.Id == userId))

        }
*/
    }
}