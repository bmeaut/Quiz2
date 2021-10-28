using System.Collections.Generic;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IStatService
    {
        public int GetNumberOfUserAnswers(int gameId, int answerId);
        public List<GameStatDto> GetOwnedGameHistory(string ownerId);
        public int GetUserPointsInGame(int gameId, string userId);
        public List<GameStatDto> GetPlayedGameHistory(string userId);
        public List<CorrectedQuestionDto> GetQuestionsOfPlayedGame(int gameId, string userId);
    }
}