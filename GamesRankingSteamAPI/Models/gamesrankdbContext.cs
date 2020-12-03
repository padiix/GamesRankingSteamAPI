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

                entity.HasIndex(e => e.GenreId)
                    .HasName("fk_Games_Genres_idx");

                entity.Property(e => e.Pegi).HasColumnName("PEGI");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Games_Genres");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GenreId)
                    .HasName("PRIMARY");

                entity.ToTable("genres");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Top10populargames>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PRIMARY");

                entity.ToTable("top10populargames");

                entity.Property(e => e.FirstReleaseDate).HasColumnType("date");

                entity.Property(e => e.Pegi).HasColumnName("PEGI");

                entity.Property(e => e.Summary)
                    .HasMaxLength(500)
                    .HasComment("opis - opis gry video (kopiuj wklej z strony internetowej zawierającej informacje o grze)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasComment("nazwa_gra - nazwa gry video");

                entity.Property(e => e.Updated).HasColumnType("date");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(150)
                    .HasComment("adresww - adres strony internetowej gry video");
            });

            modelBuilder.Entity<Top15interestinggames>(entity =>
            {
                entity.HasKey(e => e.GameId)
                    .HasName("PRIMARY");

                entity.ToTable("top15interestinggames");

                entity.HasIndex(e => e.GenreId)
                    .HasName("fk_Top15InterestingGames_Genres1_idx");

                entity.Property(e => e.FirstReleaseDate).HasColumnType("date");

                entity.Property(e => e.Pegi).HasColumnName("PEGI");

                entity.Property(e => e.Summary).HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Updated).HasColumnType("date");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Top15interestinggames)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Top15InterestingGames_Genres1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
