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

        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Top10populargames> Top10populargames { get; set; }
        public virtual DbSet<Top15interestinggames> Top15interestinggames { get; set; }
        public virtual DbSet<Top15interestinggamesHasGenres> Top15interestinggamesHasGenres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=gamesrankdb;user=user;password=password;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Top15interestinggamesHasGenres>(entity =>
            {
                entity.HasKey(e => new { e.Top15interestinggamesGameId, e.GenresGenreId })
                    .HasName("PRIMARY");

                entity.ToTable("top15interestinggames_has_genres");

                entity.HasIndex(e => e.GenresGenreId)
                    .HasName("fk_top15interestinggames_has_genres_genres1_idx");

                entity.HasIndex(e => e.Top15interestinggamesGameId)
                    .HasName("fk_top15interestinggames_has_genres_top15interestinggames_idx");

                entity.Property(e => e.Top15interestinggamesGameId).HasColumnName("top15interestinggames_GameId");

                entity.Property(e => e.GenresGenreId).HasColumnName("genres_GenreId");

                entity.HasOne(d => d.GenresGenre)
                    .WithMany(p => p.Top15interestinggamesHasGenres)
                    .HasForeignKey(d => d.GenresGenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_top15interestinggames_has_genres_genres1");

                entity.HasOne(d => d.Top15interestinggamesGame)
                    .WithMany(p => p.Top15interestinggamesHasGenres)
                    .HasForeignKey(d => d.Top15interestinggamesGameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_top15interestinggames_has_genres_top15interestinggames");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
