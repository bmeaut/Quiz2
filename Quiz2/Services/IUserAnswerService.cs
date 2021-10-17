using System.Collections.Generic;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IUserAnswerService
    {
        public UserAnswer CreateUserAnswer(string joinId, int answerId, string applicationUserId);
        public  List<int> GetCurrentQuestionStat(string joinId, ApplicationDbContext applicationDbContext = null);
        public int GetUserPoint(string joinId, string userId, ApplicationDbContext applicationDbContext = null);

        public bool IsMarked(int answerId, int gameId, string applicationUserId,
            ApplicationDbContext applicationDbContext = null);
    }
}