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
    public class QuestionController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Question/5
        [HttpGet("{questionId}")]
        public async Task<ActionResult<Question>> GetQuestion(int questionId)
        {
            return await _context.Questions.FindAsync(questionId);
        }

        // PUT: api/Question/5
        [HttpPut("{questionId}")]
        public async Task<ActionResult<List<Question>>> CreateQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return await _context.Questions.ToListAsync<Question>();
        }

        //PATCH: api/Question/5
        [HttpPatch("{questionId}")]
        public async Task<ActionResult<Question>> UpdateQuestion(int questionId)
        {
            _context.Questions.Update(await _context.Questions.FindAsync(questionId));
            await _context.SaveChangesAsync();
            return await _context.Questions.FindAsync(questionId);
        }

        //DELETE: api/Question/5
        [HttpDelete("{questionId}")]
        public async Task<IActionResult> DeleteQuestion(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
