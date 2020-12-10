using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top15interestinggamesHasGenres
    {
        public long? Top15interestinggamesGameId { get; set; }
        public long? GenresGenreId { get; set; }

        public virtual Genres GenresGenre { get; set; }
        public virtual Top15interestinggames Top15interestinggamesGame { get; set; }
    }
}
