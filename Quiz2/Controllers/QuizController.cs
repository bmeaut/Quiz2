using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuizController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("quizId")]
        public async Task<ActionResult<Quiz>> GetQuiz(int quizId)
        {
            return await _context.Quizzes.FindAsync(quizId);
        }

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
        [HttpPatch("{quizId}")]
        public async Task<ActionResult<Quiz>> UpdateQuiz(int quizId)
        {
            _context.Quizzes.Update(await _context.Quizzes.FindAsync(quizId));
            await _context.SaveChangesAsync();
            return await _context.Quizzes.FindAsync(quizId);
        }
        [HttpPut]
        public async Task<ActionResult<List<Quiz>>> CreateQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return await _context.Quizzes.ToListAsync<Quiz>();
        }
        [HttpGet]
        public async Task<ActionResult<List<Quiz>>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync<Quiz>();
        }
    }
}
