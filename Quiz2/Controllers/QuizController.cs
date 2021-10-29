using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Helper;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // GET: api/Quiz/5
        [HttpGet("{quizId}")]
        public ActionResult<Quiz> GetQuiz(int quizId)
        {
            if (UserIdCheck(quizId))
            {
                return _quizService.GetQuiz(quizId);
            }
            return null;
        }

        // DELETE: api/Quiz/5
        [HttpDelete("{quizId}")]
        public IActionResult DeleteQuiz(int quizId)
        {
            if (UserIdCheck(quizId))
            {
                _quizService.DeleteQuiz(quizId);
            }
            return NoContent();
        }

        // PUT: api/Quiz/5
        [HttpPut("{quizId}")]
        public ActionResult<Quiz> UpdateQuiz(int quizId, UpdateQuizDto updateQuizDto)
        {
            if (UserIdCheck(quizId))
            {
                return _quizService.UpdateQuiz(quizId, updateQuizDto);
            }
            return null;
        }

        // PUT: api/Quiz
        [HttpPut]
        public ActionResult<Quiz> CreateQuiz(CreateQuizDto createQuizDto)
        { 
            return _quizService.CreateQuiz(createQuizDto, HttpContext.User.GetUserId());
        }

        // GET: api/Quiz
        [HttpGet]
        public ActionResult<List<Quiz>> GetQuizzes()
        {
            return _quizService.GetQuizzes(HttpContext.User.GetUserId());
        }

        // GET: api/Quiz/5/questions
        [HttpGet("{quizId}/questions")]
        public ActionResult<List<Question>> GetQuestions(int quizId)
        {
            if (UserIdCheck(quizId))
            {
                return _quizService.GetQuestions(quizId);
            }
            return null;
        }

        private bool UserIdCheck(int quizId)
        {
            var quiz = _quizService.GetQuiz(quizId);
            return quiz.Owner.Id == HttpContext.User.GetUserId();
        }
    }
}
