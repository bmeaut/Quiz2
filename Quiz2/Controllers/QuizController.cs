using System;
using System.Collections.Generic;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService quizService;

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        // GET: api/Quiz/5
        [HttpGet("{quizId}")]
        public ActionResult<Quiz> GetQuiz(int quizId)
        {
            return quizService.GetQuiz(quizId);
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{quizId}")]
        public IActionResult DeleteQuiz(int quizId)
        {
            quizService.DeleteQuiz(quizId);
            return NoContent();
        }

        // PUT: api/Quiz/5
        [HttpPut("{quizId}")]
        public ActionResult<Quiz> UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto)
        {
            return quizService.UpdateQuiz(quizId, updateQuizDto);
        }

        // PUT: api/Quiz
        [HttpPut]
        public ActionResult<Quiz> CreateQuiz(CreateQuizDto createQuizDto)
        { 
            return quizService.CreateQuiz(createQuizDto, HttpContext.User.Identity.GetSubjectId());
        }

        // GET: api/Quiz
        [HttpGet]
        public ActionResult<List<Quiz>> GetQuizzes()
        {
            return quizService.GetQuizzes();
        }

        // GET: api/Quiz/5/questions
        [HttpGet("{quizId}/questions")]
        public ActionResult<List<Question>> GetQuestions(int quizId)
        {
            return quizService.GetQuestions(quizId);
        }
    }
}
