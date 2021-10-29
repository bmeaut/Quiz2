using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class QuestionController: ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;

        public QuestionController(IQuestionService questionService, IQuizService quizService)
        {
            _questionService = questionService;
            _quizService = quizService;
        }

        // GET: api/Question/5
        [HttpGet("{questionId}")]
        public ActionResult<Question> GetQuestion(int questionId)
        {
            return _questionService.GetQuestion(questionId);
        }

        // PUT: api/Question
        [HttpPut]
        public ActionResult<Question> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            return _questionService.CreateQuestion(createQuestionDto);
        }

        //PATCH: api/Question/5
        [HttpPatch("{questionId}")]
        public ActionResult<Question> UpdateQuestion(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            if (UserIdCheck(questionId))
            {
                return _questionService.UpdateQuestion(questionId, updateQuestionDto);
            }
            return null;
        }

        //DELETE: api/Question/5
        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(int questionId)
        {
            if (UserIdCheck(questionId))
            {
                _questionService.DeleteQuestion(questionId);   
            }
            return NoContent();
        }
        
        private bool UserIdCheck(int questionId)
        {
            var question = _questionService.GetQuestion(questionId);
            var quiz = _quizService.GetQuiz(question.QuizId);
            return quiz.Owner.Id == HttpContext.User.GetUserId();
        }
    }
}