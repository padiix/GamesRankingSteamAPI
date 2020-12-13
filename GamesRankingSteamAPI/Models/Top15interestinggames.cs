using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top15interestinggames
    {
        public Top15interestinggames()
        {
            Top15interestinggamesHG = new HashSet<Top15interestinggamesHasGenres>();
        }

        public long? GameId { get; set; }
        public string Title { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public string Url { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Top15interestinggamesHasGenres> Top15interestinggamesHG { get; set; }
    }
}
