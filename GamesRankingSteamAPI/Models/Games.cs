using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Games
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int? Pegi { get; set; }
        public string Url { get; set; }

        public virtual Genres Genre { get; set; }
    }
}
