using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GamesRankingSteamAPI.Models;

namespace GamesRankingSteamAPI.Views.Home
{
    public class _listgroupModel : PageModel
    {
        private readonly gamesrankdbContext _db;

        public _listgroupModel(gamesrankdbContext db)
        {
            _db = db;
        }

        public IEnumerable<Top15interestinggames> Interestinggames { get; set; }
        public IEnumerable<Genres> Genres { get; set; }

        public IEnumerable<Top15interestinggamesHasGenres> InterestinggamesHasGenres { get; set; }

        public async Task OnGet()
        {
            Interestinggames = await _db.Top15interestinggames.ToListAsync();
            Genres = await _db.Genres.ToListAsync();
            InterestinggamesHasGenres = await _db.Top15interestinggamesHasGenres.ToListAsync();

            foreach (var item in Interestinggames) {
                //var GamesWithGenres = _db.Top15interestinggames.Include(gam => gam.Top15interestinggamesHG).ThenInclude(gen => gen.GenresGenre).First(gam => gam.GameId == item.GameId);
                //var GenresToGame = GamesWithGenres.Top15interestinggamesHG.Select(row => row.GenresGenre);
                var GenresToGame = await _db.Genres.Where(genre => genre.Top15interestinggamesHG.Any(j => j.Top15interestinggamesGameId == item.GameId)).ToListAsync();
            }
        }
    }
}
