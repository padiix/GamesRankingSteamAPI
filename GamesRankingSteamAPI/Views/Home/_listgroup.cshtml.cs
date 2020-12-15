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

        public async Task OnGet()
        {
            return;
        }
    }
}
