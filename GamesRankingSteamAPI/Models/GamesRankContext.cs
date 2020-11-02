using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace mysqlefcore
{
    public class GamesRankContext : DbContext
    {
        public DbSet<Gry> Games { get; set; }
        public DbSet<RodzajeGier> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=GameRankDbase;user=user;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gry>(entity =>
            {
                entity.HasKey(e => e.GraID);
                entity.Property(e => e.Tytul).IsRequired();
            });

            modelBuilder.Entity<RodzajeGier>(entity =>
            {
                entity.HasKey(e => e.RodzajeGierID);
                entity.Property(e => e.NazwaRodzaju).IsRequired();
            });

            modelBuilder.Entity<InformacjeOGrze>(entity =>
            {
                entity.HasMany(d => d.Collection_Games)
                      .WithOne(p => p.FK_GamesIDs);
                entity.HasMany(d => d.Collection_Genres)
                      .WithOne(d => d.FK_GenresIDs);
            });
        }
    }
}
