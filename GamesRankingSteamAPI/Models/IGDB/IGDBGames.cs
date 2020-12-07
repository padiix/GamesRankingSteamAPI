using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesRankingSteamAPI.Models.IGDB
{
    public class IGDBGames
    {
        public int id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public DateTime first_release_date { get; set; }
        public double rating { get; set; }
        public int rating_count { get; set; }
        public int follows { get; set; }
        public string url { get; set; }
    }
}
