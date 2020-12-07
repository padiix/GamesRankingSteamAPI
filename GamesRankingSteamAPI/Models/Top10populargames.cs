using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top10populargames
    {
        public long? GameId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public double? Rating { get; set; }
        public int? RatingCount { get; set; }
        public DateTime? Updated { get; set; }
    }
}
