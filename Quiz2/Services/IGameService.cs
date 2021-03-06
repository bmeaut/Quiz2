using System.Collections.Generic;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IGameService
    {
        public Game GetGameByJoinId(string joinId);
        public Game GetGameWithQuestionsByJoinId(string joinId);
        public Game GetGameByJoinIdWithCurrentQuestion(string joinId);
        public void SetNextQuestion(Game game);
        public Game CreateGame(int quizId, string applicationUserId);
        public void AddJoinedUser(int gameId, string applicationUserId);
        public  IEnumerable<PlayerDto> GetJoinedUsersNames(int gameId);
        public void Save();
    }
}