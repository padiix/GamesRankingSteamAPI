using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Genres
    {
        public long? GenreId { get; set; }
        public string Name { get; set; }
        public long? GamesGameId { get; set; }
        public long? Top15interestinggamesGameId { get; set; }

        public virtual Games GamesGame { get; set; }
        public virtual Top15interestinggames Top15interestinggamesGame { get; set; }
    }
}
