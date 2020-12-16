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
        private readonly gamesrankdbContext _context;

        public TopPopularGamesController(gamesrankdbContext context)
        {
            _context = context;
        }

        // GET: TopPopularGames
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _context.Top10populargames.ToListAsync() });
        }
    }
}
