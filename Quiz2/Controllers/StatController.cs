using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz2.DTO;
using Quiz2.Helper;
using Quiz2.Models;
using Quiz2.Services;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatController : ControllerBase
    {
        private readonly IStatService _statService;

        public StatController(IStatService statService)
        {
            _statService = statService;
        }
        
        //GET: api/Stat/GetNumberOfUserAnswers
        [Route("GetNumberOfUserAnswers")]
        [HttpGet]
        public ActionResult<int> GetNumberOfUserAnswers(int gameId, int answerId)
        {
            return _statService.GetNumberOfUserAnswers(gameId, answerId);
        }
        
        //GET: api/Stat/GetOwnedGameHistory
        [Route("GetOwnedGameHistory")]
        [HttpGet]
        public ActionResult<List<GameStatDto>> GetOwnedGameHistory()
        {
            return _statService.GetOwnedGameHistory(HttpContext.User.GetUserId());
        }
        
        //GET: api/stat/GetUserPointsInGame
        [Route("GetUserPointsInGame")]
        [HttpGet]
        public ActionResult<int> GetUserPointsInGame(int gameId, string userId)
        {
            return _statService.GetUserPointsInGame(gameId, userId);
        }

        //GET: api/Stat/GetPlayedGameHistory
        [Route("GetPlayedGameHistory")]
        [HttpGet]
        public ActionResult<List<GameStatDto>> GetPlayedGameHistory()
        {
            return _statService.GetPlayedGameHistory(HttpContext.User.GetUserId());
        }

        //GET: api/Stat/{gameId}/GetQuestionsOfPlayedGame
        [Route("{gameId}/GetQuestionsOfPlayedGame")]
        [HttpGet]
        public ActionResult<List<CorrectedQuestionDto>> GetQuestionsOfPlayedGame(int gameId)
        {
            return _statService.GetQuestionsOfPlayedGame(gameId, HttpContext.User.GetUserId());
        }

        //GET: api/Stat/{gameId}/GetUsersOfPlayedGame
        [Route("{gameId}/GetUsersOfPlayedGame")]
        [HttpGet]
        public ActionResult<List<PlayerDto>> GetUsersOfPlayedGame(int gameId)
        {
            return _statService.GetUsersOfPlayedGame(gameId);
        }

        //GET: api/Stat/{gameId}/{userId}/GetQuestionsOfPlayedGameWithUserId
        [Route("{gameId}/{userId}/GetQuestionsOfPlayedGameWithUserId")]
        [HttpGet]
        public ActionResult<List<CorrectedQuestionDto>> GetUsersOfPlayedGameWithUserId(int gameId, string userId)
        {
            return _statService.GetQuestionsOfPlayedGame(gameId, userId);
        }



    }
}