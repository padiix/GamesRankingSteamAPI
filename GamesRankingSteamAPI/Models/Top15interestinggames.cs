using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top15interestinggames
    {
        public long? GameId { get; set; }
        public string Title { get; set; }
        public long? GenreId { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public string Url { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Genres Genre { get; set; }
    }
}
