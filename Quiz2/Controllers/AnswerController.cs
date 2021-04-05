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
    public class AnswerController: ControllerBase
    {
        private readonly IAnswerService answerService;

        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        // GET: api/Answer/5
        [HttpGet("{answerId}")]
        public ActionResult<Answer> GetAnswer(int answerId)
        {
            return answerService.GetAnswer(answerId);
        }

        // DELETE: api/Answer/5
        [HttpDelete("{answerId}")]
        public IActionResult DeleteAnswer(int answerId)
        {
            var answer = answerService.GetAnswer(answerId);
            if (answer == null)
            {
                return NotFound();
            }
            answerService.DeleteAnswer(answerId);
            return NoContent();
        }

        // PATCH: api/Answer/5
        [HttpPatch("{answerId}")]
        public ActionResult<Answer> UpdateAnswer(int answerId, UpdateAnswerDto updateAnswerDto)
        {
            return answerService.UpdateAnswer(answerId, updateAnswerDto);
        }
        
        //PUT: api/Answer
        [HttpPut]
        public ActionResult<Answer> CreateAnswer(CreateAnswerDto createAnswerDto)
        {
            return answerService.CreateAnswer(createAnswerDto);
        }
        

    }
}
