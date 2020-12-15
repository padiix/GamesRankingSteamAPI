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
    [Route("api/IntGam.json")]
    [ApiController]

    public class T15IGController : Controller
    {
        public T15IGController(){}

        [HttpGet]
        // GET: T15IG
        public async Task<IActionResult> GetAll()
        {
            using gamesrankdbContext db = new gamesrankdbContext();
            List<Top15interestinggames> TIGames = await db.Top15interestinggames.ToListAsync();
            List<Top15interestinggamesHasGenres> TIGHGenres = await db.Top15interestinggamesHasGenres.ToListAsync();
            List<Genres> Genres = await db.Genres.ToListAsync();

            return Json(new { data = TIGames });
        }
    }
}
