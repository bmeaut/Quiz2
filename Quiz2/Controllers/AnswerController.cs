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
    public class AnswerController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnswerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{answerId}")]
        public async Task<ActionResult<Answer>> GetAnswer(int answerId)
        {
            return await _context.Answers.FindAsync(answerId);
        }

        [HttpDelete("{answerId}")]
        public async Task<IActionResult> DeleteAnswer(int answerId)
        {
            var answer = await _context.Answers.FindAsync(answerId);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{answerId}")]
        public async Task<ActionResult<Answer>> UpdateAnswer(int answerId)
        {
            _context.Answers.Update(await _context.Answers.FindAsync(answerId));
            await _context.SaveChangesAsync();
            return await _context.Answers.FindAsync(answerId);
        }

    }
}
