using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mysqlefcore
{
    public class Gry
    {
        public Gry(){}

        public int GraID { get; set; }
        public string Tytul { get; set; }
        public string Opis { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataWydania { get; set; }
        public decimal Cena { get; set; }
        public string AdresInternetowy { get; set; }
        public int PEGI { get; set; }
        public int PktPopularnosci { get; set; }
    }

    public class RodzajeGier
    {
        public RodzajeGier(){}

        public int RodzajeGierID { get; set; }
        public string NazwaRodzaju { get; set; }
        public string Opis { get; set; }
    }

    public class InformacjeOGrze {
        public InformacjeOGrze(){}

        public virtual Gry Gra { get; set; }
        public virtual RodzajeGier RodzajGry { get; set; }

        public void LinkGameAndGenre(Gry Gra, RodzajeGier Rodzaj)
        {
            this.Gra = Gra;
            this.RodzajGry = Rodzaj;
        }
    }
}
