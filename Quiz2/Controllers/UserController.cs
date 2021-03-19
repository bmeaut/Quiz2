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
    //[Authorize]
    public class UserController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<ApplicationUser>> GetApplicationUser(int userId)
        {
            return await _context.ApplicationUsers.FindAsync(userId);
        }
        // PATCH: api/User/5
        [HttpPatch("{userId}")]
        public async Task<ActionResult<ApplicationUser>> UpdateApplicationUser(int userId)
        {
            _context.ApplicationUsers.Update(await _context.ApplicationUsers.FindAsync(userId));
            await _context.SaveChangesAsync();
            return await _context.ApplicationUsers.FindAsync(userId);
        }

        //GET: api/User/5/games
        [HttpGet("{userId}/games")]
        public async Task<ActionResult<List<Game>>> GetGames(int userId)
        {
            var user = _context.ApplicationUsers.Find(userId);
            return _context.Games.Where(e => e.JoinedUsers.Contains(user)).ToList();            
        }

        //GET: api/User/joingame
        [HttpGet("joingame")]
        public async Task<ActionResult<Game>> JoinGame(string joinId)
        {
            // join by string id provided
            return _context.Games.Where(q => q.JoinId.Equals(joinId)).First();
        }

    }
}
