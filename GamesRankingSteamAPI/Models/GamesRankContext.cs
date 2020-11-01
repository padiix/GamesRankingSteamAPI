using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace mysqlefcore
{
    public class GamesRankContext : DbContext
    {
        public DbSet<Games> Games { get; set; }
        public DbSet<Genres> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=GameRankDbase;user=user;password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<GameInfo>(entity =>
            {
                entity.HasMany(d => d.Collection_Games)
                      .WithOne(p => p.FK_GamesIDs);
                entity.HasMany(d => d.Collection_Genres)
                      .WithOne(d => d.FK_GenresIDs);
            });
        }
    }
}
