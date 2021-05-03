using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz2.Services;

namespace Quiz2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatController : ControllerBase
    {
        private readonly IStatService statService;

        public StatController(IStatService statService)
        {
            this.statService = statService;
        }
        
        //GET: api/Stat/
        [HttpGet]
        public ActionResult<int> GetNumberOfUserAnswers(int gameId, int answerId)
        {
            return statService.GetNumberOfUserAnswers(gameId, answerId);
        }
    }
}