
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top15interestinggamesHasGenres
    {
        public long? Top15interestinggamesGameId { get; set; }
        public long? GenresGenreId { get; set; }

        public virtual Genres GenresGenre { get; set; }
        [JsonIgnore]
        public virtual Top15interestinggames Top15interestinggamesGame { get; set; }
    }
}
