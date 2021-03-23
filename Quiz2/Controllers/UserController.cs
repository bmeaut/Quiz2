using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz2.Data;
using Quiz2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz2.Services;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService applicationUserService;

        public UserController(IApplicationUserService applicationUserService,ApplicationDbContext context)
        {
            _context = context;
            this.applicationUserService = applicationUserService;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<List<ApplicationUser>> GetUsers()
        {
            return _context.ApplicationUsers.ToList();
        }
        
        // GET: api/User/5
        [HttpGet("{userId}")]
        public ActionResult<ApplicationUser> GetApplicationUser(string userId)
        {
            return applicationUserService.GetUser(userId);
        }
        // PATCH: api/User/5
        [HttpPatch("{userId}")]
        public ActionResult<ApplicationUser> UpdateApplicationUser(string userId)
        {
            _context.ApplicationUsers.Update(_context.ApplicationUsers.Find(userId)); 
            _context.SaveChanges();
            return _context.ApplicationUsers.Find(userId);
        }

        //GET: api/User/5/games
        [HttpGet("{userId}/games")]
        public ActionResult<List<Game>> GetGames(string userId)
        {
            var user = _context.ApplicationUsers.Find(userId);
            return _context.Games.Where(e => e.JoinedUsers.Contains(user)).ToList();            
        }

        //GET: api/User/joingame
        [HttpGet("joingame")]
        public ActionResult<Game> JoinGame(string joinId)
        {
            // join by string id provided
            return _context.Games.Where(q => q.JoinId.Equals(joinId)).First();
        }

    }
}