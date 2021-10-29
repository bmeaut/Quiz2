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

        public AnswerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Answer GetAnswer(int answerId)
        {
            return _context.Answers
                .Include(answer => answer.Question)
                .FirstOrDefault(answer => answer.Id == answerId);
        }

        public void DeleteAnswer(int answerId)
        {
            var answer = _context.Answers.Find(answerId);
            if (answer == null)
            {
                return;
            }
            _context.Remove(answer);
            _context.SaveChanges();

        }
        public Answer CreateAnswer(CreateAnswerDto createAnswerDto)
        {
            if (_context.Questions.Any(q => q.Id == createAnswerDto.QuestionId))
            {
                var answer = new Answer()
                    {
                        Correct = createAnswerDto.Correct,
                        Text = createAnswerDto.Text,
                        QuestionId = createAnswerDto.QuestionId
                    };
                     _context.Answers.Add(answer);
                    _context.SaveChanges();
                    return answer;
            }

            return null;
        }
    }
}