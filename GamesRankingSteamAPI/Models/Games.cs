using System;
using System.Collections.Generic;

namespace GamesRankingSteamAPI.Models
{
    public partial class Games
    {
        public Games()
        {
            Genres = new HashSet<Genres>();
        }

        public long? GameId { get; set; }
        public string Title { get; set; }
        public int? Pegi { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }

        public virtual ICollection<Genres> Genres { get; set; }
    }
}
