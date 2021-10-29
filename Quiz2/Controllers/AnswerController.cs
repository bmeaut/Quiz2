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
        private readonly IAnswerService _answerService;
        private readonly IQuizService _quizService;
        private readonly IQuestionService _questionService;

        public AnswerController(IAnswerService answerService, IQuizService quizService, IQuestionService questionService)
        {
            _answerService = answerService;
            _quizService = quizService;
            _questionService = questionService;
        }

        // GET: api/Answer/5
        [HttpGet("{answerId}")]
        public ActionResult<Answer> GetAnswer(int answerId)
        {
            return _answerService.GetAnswer(answerId);
        }

        // DELETE: api/Answer/5
        [HttpDelete("{answerId}")]
        public IActionResult DeleteAnswer(int answerId)
        {
            if (UserIdCheck(answerId))
            {
                _answerService.DeleteAnswer(answerId);
            }
            return NoContent();
        }

        //PUT: api/Answer
        [HttpPut]
        public ActionResult<Answer> CreateAnswer(CreateAnswerDto createAnswerDto)
        {
            return _answerService.CreateAnswer(createAnswerDto);
        }
        private bool UserIdCheck(int answerId)
        {
            var answer = _answerService.GetAnswer(answerId);
            var question = _questionService.GetQuestion(answer.QuestionId);
            var quiz = _quizService.GetQuiz(question.QuizId);
            return quiz.Owner.Id == HttpContext.User.GetUserId();
        }

    }
}
