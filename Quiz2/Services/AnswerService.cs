using System.Linq;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class AnswerService: IAnswerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuestionService questionService;

        public AnswerService(ApplicationDbContext context, IQuestionService questionService)
        {
            _context = context;
            this.questionService = questionService;
        }
        
        public Answer GetAnswer(int answerId)
        {
            return _context.Answers.Include(answer => answer.Question)
                .Where(answer => answer.Id.Equals(answerId))
                .First();
        }

        public void DeleteAnswer(int answerId)
        {
            _context.Remove(_context.Answers.Find(answerId));
            _context.SaveChanges();
        }


        public Answer UpdateAnswer(int answerId, UpdateAnswerDto updateAnswerDto)
        {
            var answer = _context.Answers.Include(answer => answer.Question)
                .First(answer => answer.Id.Equals(answerId));
            answer.Text = updateAnswerDto.Text;
            answer.Correct = updateAnswerDto.Correct;
            _context.SaveChanges();
            return answer;
        }

        public Answer CreateAnswer(CreateAnswerDto createAnswerDto)
        {
            var answer = new Answer();
            answer.Correct = createAnswerDto.Correct;
            answer.Text = createAnswerDto.Text;
            answer.Question = questionService.GetQuestion(createAnswerDto.QuestionId);
            _context.Answers.Add(answer);
            _context.SaveChanges();
            return answer;
        }
    }
}