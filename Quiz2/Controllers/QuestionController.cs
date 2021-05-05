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
        private readonly IQuestionService questionService;
        private readonly IQuizService quizService;

        public QuestionController(IQuestionService questionService, IQuizService quizService)
        {
            this.questionService = questionService;
            this.quizService = quizService;
        }

        // GET: api/Question/5
        [HttpGet("{questionId}")]
        public ActionResult<Question> GetQuestion(int questionId)
        {
            return questionService.GetQuestion(questionId);
        }

        // PUT: api/Question
        [HttpPut]
        public ActionResult<Question> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            return questionService.CreateQuestion(createQuestionDto);
        }

        //PATCH: api/Question/5
        [HttpPatch("{questionId}")]
        public ActionResult<Question> UpdateQuestion(int questionId, UpdateQuestionDto updateQuestionDto)
        {
            if (UserIdCheck(questionId))
            {
                return questionService.UpdateQuestion(questionId, updateQuestionDto);
            }
            return null;
        }

        //DELETE: api/Question/5
        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(int questionId)
        {
            if (UserIdCheck(questionId))
            {
                questionService.DeleteQuestion(questionId);   
            }
            return NoContent();
        }
        
        private bool UserIdCheck(int questionId)
        {
            var question = questionService.GetQuestion(questionId);
            var quiz = quizService.GetQuiz(question.QuizId);
            return quiz.Owner.Id == HttpContext.User.GetUserId();
        }
    }
}