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
            if (answer != null)
            {
                _context.Remove(answer);
                _context.SaveChanges();
            }
            
        }


        public Answer UpdateAnswer(int answerId, UpdateAnswerDto updateAnswerDto)
        {
            var answer = _context.Answers
                .Include(a => a.Question)
                .FirstOrDefault(a => a.Id == answerId);
            if(answer != null)
            {
                answer.Text = updateAnswerDto.Text;
                answer.Correct = updateAnswerDto.Correct;
                _context.SaveChanges();
            }
            return answer;
        }

        public Answer CreateAnswer(CreateAnswerDto createAnswerDto)
        {
            var question = _context.Questions
                .Include(q => q.Quiz)
                .Include(q => q.Answers)
                .FirstOrDefault(q => q.Id == createAnswerDto.QuestionId);
            if(question != null)
            {
                var answer = new Answer()
                {
                    Correct = createAnswerDto.Correct,
                    Text = createAnswerDto.Text,
                    Question = question
                };
                question.Answers.Add(answer);
                _context.SaveChanges();
                return answer;
            }
            return null;
        }
    }
}