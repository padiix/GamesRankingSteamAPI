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
    [Route("api/IntGam")]
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

            //var gamesRecord = from e in TIGames
            //                  join d in TIGHGenres on e.GameId equals d.Top15interestinggamesGameId into table1
            //                  from d in table1.ToList()
            //                  join i in Genres on d.GenresGenreId equals i.GenreId into table2
            //                  from i in table2.ToList()
            //                  select new ViewModel
            //                  {
            //                      topinterestinggames = e,
            //                      genres = i
            //                  };
            return Json(new { data = TIGames });
        }
    }
}
