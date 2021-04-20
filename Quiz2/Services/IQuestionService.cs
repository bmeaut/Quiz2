using System.Collections.Generic;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public interface IQuestionService
    {
        public Question GetQuestion(int questionId);
        public Question CreateQuestion(CreateQuestionDto createQuestionDto);
        public Question UpdateQuestion(int questionId, UpdateQuestionDto updateQuestionDto);
        public void DeleteQuestion(int questionId);

        public Question GetNextQuestion(int gameId, int currentPosition);
    }
}