using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRankingSteamAPI.Models
{
    public class ViewModel
    {
        public Top15interestinggames topinterestinggames { get; set; }
        public Top15interestinggamesHasGenres gameshasgenres { get; set; }
        public Genres genres { get; set; }
    }
}
