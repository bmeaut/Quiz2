using System.Linq;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class QuestionService: IQuestionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuizService quizService;

        public QuestionService(IQuizService quizService, ApplicationDbContext context)
        {
            _context = context;
            this.quizService = quizService;
        }

        
        public Question GetQuestion(int questionId)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == questionId);
        }

        public Question CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            var question = new Question()
            {
                Text = createQuestionDto.Text
            };
            var quiz = quizService.GetQuiz(createQuestionDto.QuizId);
            if(quiz != null)
            {
                quiz.Questions.Add(question);
                _context.SaveChanges();
                return question;
            }
            return null;
        }

        public Question UpdateQuestion(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            var question = _context.Questions.FirstOrDefault(q => q.Id == questionId);
            if(question != null)
            {
                question.Text = updateQuestionDto.Text;
                _context.SaveChanges();
            }
            return question;
        }

        public void DeleteQuestion(int questionId)
        {
            var question = _context.Questions.Find(questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
        }
        
    }
}