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

        /*
         * Include Quiz object in the returned json
         */
        public Question GetQuestion(int questionId)
        {
            return _context.Questions.First(q => q.Id.Equals(questionId));
        }

        public Question CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            var question = new Question();
            question.Text = createQuestionDto.Text;
            var quiz = quizService.GetQuiz(createQuestionDto.QuizId);
            quiz.Questions.Add(question);
            _context.SaveChanges();
            return question;
        }

        public Question UpdateQuestion(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            var question = _context.Questions.Find(questionId);
            question.Text = updateQuestionDto.Text;
            _context.SaveChanges();
            return question;
        }

        public void DeleteQuestion(int questionId)
        {
            _context.Questions.Remove(_context.Questions.Find(questionId));
            _context.SaveChanges();
        }
        
    }
}