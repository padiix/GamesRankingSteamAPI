using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top10populargames
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public double? Rating { get; set; }
        public int? RatingCount { get; set; }
        public int Pegi { get; set; }
        public string Url { get; set; }
        public DateTime? Updated { get; set; }
    }
}
