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
        private readonly IAnswerService answerService;

        public QuestionService(IAnswerService answerService, ApplicationDbContext context)
        {
            _context = context;
            this.answerService = answerService;
        }

        
        public Question GetQuestion(int questionId)
        {
            return _context.Questions
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == questionId);
        }

        public Question CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            var quiz = _context.Quizzes
                    .Include(q => q.Owner)
                    .Include(q => q.Questions)
                    .Include(q => q.Games)
                    .FirstOrDefault(q => q.Id == createQuestionDto.QuizId);
            if(quiz != null)
            {
                var question = new Question()
                {
                    Text = createQuestionDto.Text,
                    SecondsToAnswer = createQuestionDto.SecondsToAnswer,
                    Points = createQuestionDto.Points,
                    Position = createQuestionDto.Position
                };
                quiz.Questions.Add(question);
                _context.SaveChanges();
                foreach (CreateAnswerDto createAnswerDto in createQuestionDto.CreateAnswerDtoList)
                {
                    createAnswerDto.QuestionId = question.Id;
                    answerService.CreateAnswer(createAnswerDto);
                }
                return GetQuestion(question.Id);
            }
            return null;
        }

        public Question UpdateQuestion(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            var question = _context.Questions
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == questionId);
            if(question != null)
            {
                question.Text = updateQuestionDto.Text;
                _context.SaveChanges();
            }
            return question;
        }

        public void DeleteQuestion(int questionId)
        {
            var question = _context.Questions
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == questionId);
            if (question != null)
            {
                while(question.Answers.FirstOrDefault() != null)
                {
                    answerService.DeleteAnswer(question.Answers.FirstOrDefault().Id);
                }
                _context.Questions.Remove(question);
                _context.SaveChanges();
            }
        }

        public Question GetNextQuestion(int quizId, int currentPosition)
        {
            return _context.Questions.Where(question => question.Quiz.Id == quizId)
                .Where(question => question.Position > currentPosition)
                .OrderBy(question => question.Position)
                .FirstOrDefault();
        }
        
    }
}