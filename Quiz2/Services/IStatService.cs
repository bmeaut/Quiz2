using System.Collections.Generic;
using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IStatService
    {
        public int GetNumberOfUserAnswers(int gameId, int answerId);
        public List<Game> GetOwnedGameHistory(string ownerId);
        public int GetUserPointsInGame(int gameId, string userId);
    }
}