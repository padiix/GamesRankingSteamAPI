using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Genres
    {
        public Genres()
        {
            Games = new HashSet<Games>();
            Top15interestinggames = new HashSet<Top15interestinggames>();
        }

        public long? GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Games> Games { get; set; }
        public virtual ICollection<Top15interestinggames> Top15interestinggames { get; set; }
    }
}
