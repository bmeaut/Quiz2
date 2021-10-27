using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.DTO;
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

        public List<GameStatDto> GetOwnedGameHistory(string ownerId)
        {
            return _context.Games
                .Include(game => game.Owner)
                .Include(game => game.Quiz)
                .Include(game => game.JoinedUsers)
                .Where(game => game.Owner.Id == ownerId)
                .Select(game => new GameStatDto() { Id = game.Id, QuizName = game.Quiz.Name, IsOwned = true })
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

        public List<GameStatDto> GetPlayedGameHistory(string userId)
        {
            ApplicationUser user = _context.ApplicationUsers.Find(userId);
            return _context.Games
                .Include(game => game.Owner)
                .Include(game => game.Quiz)
                .Include(game => game.JoinedUsers)
                .Where(game => game.Owner.Id != userId && game.JoinedUsers.Contains(user))
                .Select(game => new GameStatDto() { Id = game.Id, QuizName = game.Quiz.Name, IsOwned = false })
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