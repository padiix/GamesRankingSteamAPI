using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GamesRankingSteamAPI.Models
{
    public partial class gamesrankdbContext : DbContext
    {
        public gamesrankdbContext()
        {
        }

        public gamesrankdbContext(DbContextOptions<gamesrankdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Top10populargames> Top10populargames { get; set; }
        public virtual DbSet<Top15interestinggames> Top15interestinggames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=gamesrankdb;user=user;password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PRIMARY");

                entity.ToTable("games");

                entity.Property(e => e.Pegi).HasColumnName("PEGI");

                entity.Property(e => e.Summary).HasMaxLength(1000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GenreId)
                    .HasName("PRIMARY");

                entity.ToTable("genres");

                entity.HasIndex(e => e.GamesGameId)
                    .HasName("fk_genres_games_idx");

                entity.HasIndex(e => e.Top15interestinggamesGameId)
                    .HasName("fk_genres_top15interestinggames1_idx");

                entity.Property(e => e.GamesGameId).HasColumnName("games_GameId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Top15interestinggamesGameId).HasColumnName("top15interestinggames_GameId");

                entity.HasOne(d => d.GamesGame)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.GamesGameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_genres_games");

                entity.HasOne(d => d.Top15interestinggamesGame)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.Top15interestinggamesGameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_genres_top15interestinggames1");
            });

            modelBuilder.Entity<Top10populargames>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PRIMARY");

                entity.ToTable("top10populargames");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Updated).HasColumnType("date");
            });

            modelBuilder.Entity<Top15interestinggames>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PRIMARY");

                entity.ToTable("top15interestinggames");

                entity.Property(e => e.FirstReleaseDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Updated).HasColumnType("date");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
