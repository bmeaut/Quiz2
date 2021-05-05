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
    public class AnswerController: ControllerBase
    {
        private readonly IAnswerService answerService;
        private readonly IQuizService quizService;
        private readonly IQuestionService questionService;

        public AnswerController(IAnswerService answerService, IQuizService quizService, IQuestionService questionService)
        {
            this.answerService = answerService;
            this.quizService = quizService;
            this.questionService = questionService;
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
            if (UserIdCheck(answerId))
            {
                answerService.DeleteAnswer(answerId);
            }
            return NoContent();
        }

        //PUT: api/Answer
        [HttpPut]
        public ActionResult<Answer> CreateAnswer(CreateAnswerDto createAnswerDto)
        {
            return answerService.CreateAnswer(createAnswerDto);
        }
        private bool UserIdCheck(int answerId)
        {
            var answer = answerService.GetAnswer(answerId);
            var question = questionService.GetQuestion(answer.QuestionId);
            var quiz = quizService.GetQuiz(question.QuizId);
            return quiz.Owner.Id == HttpContext.User.GetUserId();
        }

    }
}
