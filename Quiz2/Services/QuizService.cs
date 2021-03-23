using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz2.Services
{
    public class QuizService: IQuizService
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService applicationUserService;

        public QuizService(IApplicationUserService applicationUserService, ApplicationDbContext context)
        {
            _context = context;
            this.applicationUserService = applicationUserService;
        }

        public Quiz GetQuiz(int quizId)
        {
            return _context.Quizzes.Find(quizId);
        }

        public Quiz CreateQuiz(CreateQuizDto createQuizDto)
        {
            var owner = applicationUserService.GetUser(createQuizDto.OwnerId);
            var quiz = new Quiz();
            quiz.Name = createQuizDto.Name;
            quiz.Owner = owner;
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return quiz;
        }

        public List<Quiz> GetQuizzes()
        {
            return _context.Quizzes.ToList();
        }

        public List<Question> GetQuestions(int quizId)
        {
            return _context.Quizzes.Find(quizId).Questions.ToList();
        }

        public Quiz UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto)
        {
            var quiz = _context.Quizzes.Find(quizId);
            quiz.Name = updateQuizDto.Name;
            _context.SaveChanges();
            return quiz;
        }

        public void DeleteQuiz(int quizId)
        {
            _context.Quizzes.Remove(_context.Quizzes.Find(quizId));
            _context.SaveChanges();
        }
    }
}
