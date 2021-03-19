using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz2.Services
{
    public class QuizService
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUserService applicationUserService;

        public QuizService(ApplicationUserService applicationUserService, ApplicationDbContext context)
        {
            _context = context;
            this.applicationUserService = applicationUserService;
        }

        public Quiz GetQuiz(int quizId)
        {
            return _context.Quizzes.Find(quizId);
        }

        public Quiz CreateQuiz(CreateQuizDTO createQuizDTO)
        {
            var owner = applicationUserService.GetUser(createQuizDTO.OwnerId);
            var quiz = new Quiz();
            quiz.Name = createQuizDTO.Name;
            quiz.Owner = owner;
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
            return quiz;
        }
    }
}
