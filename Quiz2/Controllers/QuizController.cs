using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.Models;
using System;
using Quiz2.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz2.DTO;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly QuizService quizService;

        public QuizController(QuizService quizService, ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> DeleteQuiz(int quizId)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/Quiz/5
        [HttpPut("{quizId}")]
        public async Task<ActionResult<Quiz>> UpdateQuiz(int quizId)
        {
            _context.Quizzes.Update(await _context.Quizzes.FindAsync(quizId));
            await _context.SaveChangesAsync();
            return await _context.Quizzes.FindAsync(quizId);
        }

        // PUT: api/Quiz
        [HttpPut]
        public ActionResult<Quiz> CreateQuiz(CreateQuizDTO createQuizDTO)
        {
            return quizService.CreateQuiz(createQuizDTO);
        }

        // GET: api/Quiz
        [HttpGet]
        public async Task<ActionResult<List<Quiz>>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync<Quiz>();
        }

        // GET: api/Quiz/5/questions
        [HttpGet("{quizId}/questions")]
        public async Task<ActionResult<List<Question>>> GetQuestions(int quizId)
        { 
            var quiz = await _context.Quizzes.FindAsync(quizId);
            return quiz.Questions.ToList<Question>();
        }
    }
}
