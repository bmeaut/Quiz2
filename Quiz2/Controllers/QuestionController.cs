using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QuestionController: ControllerBase
    {
        private readonly IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
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
            return questionService.UpdateQuestion(questionId, updateQuestionDto);
        }

        //DELETE: api/Question/5
        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(int questionId)
        {
            var question = questionService.GetQuestion(questionId);
            if (question == null)
            {
                return NotFound();
            }
            questionService.DeleteQuestion(questionId);
            return NoContent();
        }
    }
}