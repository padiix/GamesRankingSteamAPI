using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Top15interestinggames
    {
        public Top15interestinggames()
        {
            Genres = new HashSet<Genres>();
        }

        public long? GameId { get; set; }
        public string Title { get; set; }
        public DateTime? FirstReleaseDate { get; set; }
        public string Url { get; set; }
        public DateTime? Updated { get; set; }

        public virtual ICollection<Genres> Genres { get; set; }
    }
}
