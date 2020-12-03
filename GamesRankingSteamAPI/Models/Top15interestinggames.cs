using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top15interestinggames
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int GenreId { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public double? TotalRating { get; set; }
        public int? Follows { get; set; }
        public int? Pegi { get; set; }
        public string Url { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Genres Genre { get; set; }
    }
}
