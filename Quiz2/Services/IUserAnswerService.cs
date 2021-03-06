using System.Collections.Generic;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IUserAnswerService
    {
        public UserAnswer CreateUserAnswer(string joinId, int answerId, string applicationUserId);
        public CurrentQuestionStatDto getCurrentQuestionStat(string joinId);
    }
}