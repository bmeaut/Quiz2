using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController: ControllerBase
    {
        private readonly IApplicationUserService applicationUserService;

        public UserController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<List<ApplicationUser>> GetUsers()
        {
            return applicationUserService.GetUsers();
        }
        
        // GET: api/User/5
        [HttpGet("{userId}")]
        public ActionResult<ApplicationUser> GetApplicationUser(string userId)
        {
            return applicationUserService.GetUser(userId);
        }
        // PATCH: api/User/5
        [HttpPatch("{userId}")]
        public ActionResult<ApplicationUser> UpdateApplicationUser(string userId, UpdateApplicationUserDto updateApplicationUserDto)
        {
            return applicationUserService.UpdateApplicationUser(userId, updateApplicationUserDto);
        }

        //GET: api/User/5/games
        [HttpGet("{userId}/games")]
        public ActionResult<List<Game>> GetGames(string userId)
        {
            return applicationUserService.GetGames(userId);
        }

        //GET: api/User/joingame
        [HttpGet("joingame")]
        public ActionResult<Game> JoinGame(string joinId)
        {
            // join by string id provided
            return applicationUserService.JoinGame(joinId);
        }

    }
}