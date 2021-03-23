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
        public ActionResult<Question> GetQuestion(int questionId)
        {
            return _context.Questions.Find(questionId);
        }

        // PUT: api/Question/5
        [HttpPut("{questionId}")]
        public ActionResult<List<Question>> CreateQuestion(Question question)
        {
            _context.Questions.Add(question); 
            _context.SaveChanges();
            return _context.Questions.ToList<Question>();
        }

        //PATCH: api/Question/5
        [HttpPatch("{questionId}")]
        public ActionResult<Question> UpdateQuestion(int questionId)
        {
            _context.Questions.Update( _context.Questions.Find(questionId));
            _context.SaveChanges();
            return _context.Questions.Find(questionId);
        }

        //DELETE: api/Question/5
        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(int questionId)
        {
            var question = _context.Questions.Find(questionId);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            _context.SaveChanges();

            return NoContent();
        }
    }
}