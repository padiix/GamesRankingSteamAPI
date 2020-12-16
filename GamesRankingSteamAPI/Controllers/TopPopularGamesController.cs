using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesRankingSteamAPI.Models;

namespace GamesRankingSteamAPI.Controllers
{
    [Route("api/PopGam.json")]
    [ApiController]
    public class TopPopularGamesController : Controller
    {

        public TopPopularGamesController()
        {}

        // GET: TopPopularGames
        public async Task<IActionResult> GetAll()
        {
            using gamesrankdbContext db = new gamesrankdbContext();
            return Json(new { data = await db.Top10populargames.ToListAsync() });
        }
    }
}
