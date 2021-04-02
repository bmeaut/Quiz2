using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IGameService
    {
        public Game GetGameByJoinId(string joinId);
        public Game GetGameWithQuestionsByJoinId(string joinId);
        public void Save();
    }
}