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
    public interface IQuizService
    {
        public Quiz GetQuiz(int userId);
        public Quiz CreateQuiz(CreateQuizDto createQuizDto, string currentUserId);
        public List<Quiz> GetQuizzes();
        public List<Question> GetQuestions(int quizId);
        public Quiz UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto);
        public void DeleteQuiz(int quizId);
    }
}