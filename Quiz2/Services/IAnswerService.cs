using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IAnswerService
    {
        public Answer GetAnswer(int answerId);
        public void DeleteAnswer(int answerId);
        public Answer CreateAnswer(CreateAnswerDto createAnswerDto);
    }
}