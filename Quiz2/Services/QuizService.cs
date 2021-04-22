using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Quiz2.Services
{
    public class QuizService: IQuizService
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService applicationUserService;
        private readonly IQuestionService questionService;

        public QuizService(IQuestionService questionService, IApplicationUserService applicationUserService, ApplicationDbContext context)
        {
            _context = context;
            this.applicationUserService = applicationUserService;
            this.questionService = questionService;
        }

        public Quiz GetQuiz(int quizId)
        {
            return _context.Quizzes
                    .Include(quiz => quiz.Owner)
                    .Include(quiz => quiz.Questions)
                    .Include(quiz => quiz.Games)
                    .FirstOrDefault(quiz => quiz.Id == quizId);
        }

        public Quiz CreateQuiz(CreateQuizDto createQuizDto, string currentUserId)
        {
            var owner = applicationUserService.GetUser(currentUserId);
            if(owner != null)
            {
                var quiz = new Quiz()
                {
                    Name = createQuizDto.Name,
                    Owner = owner
                };
                _context.Quizzes.Add(quiz);
                _context.SaveChanges();
                return quiz;
            }
            return null;
            
        }

        public List<Quiz> GetQuizzes()
        {
            return _context.Quizzes
                .Include(quiz => quiz.Owner)
                .Include(quiz => quiz.Questions)
                .Include(quiz => quiz.Games)
                .ToList();
        }

        public List<Question> GetQuestions(int quizId)
        {
            var quiz = _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefault(q => q.Id == quizId);
            if(quiz != null)
            {
                return quiz.Questions;
            }
            return null;
        }

        public Quiz UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto)
        {
            var quiz = _context.Quizzes
                    .Include(q => q.Owner)
                    .Include(q => q.Questions)
                    .Include(q => q.Games)
                    .FirstOrDefault(q => q.Id == quizId);
            if (quiz != null)
            {
                quiz.Name = updateQuizDto.Name;
                _context.SaveChanges();
            }
            return quiz;
        }

        public void DeleteQuiz(int quizId)
        {
            var quiz = _context.Quizzes
                    .Include(q => q.Owner)
                    .Include(q => q.Questions)
                    .Include(q => q.Games)
                    .FirstOrDefault(q => q.Id == quizId);
            if (quiz != null)
            {
                while (quiz.Questions.FirstOrDefault() == null)
                {
                    questionService.DeleteQuestion(quiz.Questions.FirstOrDefault().Id);
                }
                _context.Quizzes.Remove(quiz);
                _context.SaveChanges();
            }
        }
    }
}
