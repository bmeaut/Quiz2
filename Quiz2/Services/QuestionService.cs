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
            return _context.Questions.Include(question => question.Quiz)
                .Include(question => question.Answers)
                .First(q => q.Id.Equals(questionId));
        }

        public Question CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            var question = new Question();
            question.Text = createQuestionDto.Text;
            question.Quiz = quizService.GetQuiz(createQuestionDto.QuizId);
            _context.Questions.Add(question); 
            _context.SaveChanges();
            return question;
        }

        public Question UpdateQuestion(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            var question = _context.Questions.Find(questionId);
            question.Text = updateQuestionDto.Text;
            question.Quiz = quizService.GetQuiz(updateQuestionDto.QuizId);
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