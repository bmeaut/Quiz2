using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz2.Data;
using Quiz2.Models;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnswerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Answer/5
        [HttpGet("{answerId}")]
        public ActionResult<Answer> GetAnswer(int answerId)
        {
            return _context.Answers.Find(answerId);
        }

        // DELETE: api/Answer/5
        [HttpDelete("{answerId}")]
        public IActionResult DeleteAnswer(int answerId)
        {
            var answer = _context.Answers.Find(answerId);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            _context.SaveChanges();

            return NoContent();
        }

        // PATCH: api/Answer/5
        [HttpPatch("{answerId}")]
        public ActionResult<Answer> UpdateAnswer(int answerId)
        {
            _context.Answers.Update( _context.Answers.Find(answerId));
            _context.SaveChanges();
            return _context.Answers.Find(answerId);
        }

    }
}
