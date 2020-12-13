using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Genres
    {
        public Genres()
        {
            Top15interestinggamesHG = new HashSet<Top15interestinggamesHasGenres>();
        }

        public long? GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Top15interestinggamesHasGenres> Top15interestinggamesHG { get; set; }
    }
}
