using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Games
    {
        public long? GameId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public long? GenreId { get; set; }
        public int? Pegi { get; set; }
        public string Url { get; set; }

        public virtual Genres Genre { get; set; }
    }
}
