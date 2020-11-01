using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mysqlefcore
{
    public class Games
    {
        public Games(){}

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string wwwaddress { get; set; }
        public int PEGI { get; set; }
        public int Popularity_Points { get; set; }

        public GameInfo FK_GamesIDs { get; set; }
    }

    public class Genres
    {
        public Genres(){}

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public GameInfo FK_GenresIDs { get; set; }
    }

    public class GameInfo {
        public GameInfo()
        {
            this.Collection_Games = new HashSet<Games>();
            this.Collection_Genres = new HashSet<Genres>();
        }
        public virtual ICollection<Games> Collection_Games { get; set; }
        public virtual ICollection<Genres> Collection_Genres { get; set; }
    }
}
