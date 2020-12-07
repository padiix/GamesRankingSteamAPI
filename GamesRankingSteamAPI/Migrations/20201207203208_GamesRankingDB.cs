using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace GamesRankingSteamAPI.Migrations
{
    public partial class GamesRankingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    GenreId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "top10populargames",
                columns: table => new
                {
                    GameId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 150, nullable: false, comment: "nazwa_gra - nazwa gry video"),
                    Summary = table.Column<string>(maxLength: 500, nullable: true, comment: "opis - opis gry video (kopiuj wklej z strony internetowej zawierającej informacje o grze)"),
                    FirstReleaseDate = table.Column<DateTime>(type: "date", nullable: true),
                    Rating = table.Column<double>(nullable: true),
                    RatingCount = table.Column<int>(nullable: true),
                    Updated = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    GameId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    GenreId = table.Column<long>(nullable: true),
                    PEGI = table.Column<int>(nullable: true),
                    URL = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GameId);
                    table.ForeignKey(
                        name: "fk_Games_Genres",
                        column: x => x.GenreId,
                        principalTable: "genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "top15interestinggames",
                columns: table => new
                {
                    GameId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    GenreId = table.Column<long>(nullable: true),
                    FirstReleaseDate = table.Column<DateTime>(type: "date", nullable: true),
                    URL = table.Column<string>(maxLength: 150, nullable: true),
                    Updated = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GameId);
                    table.ForeignKey(
                        name: "fk_Top15InterestingGames_Genres1",
                        column: x => x.GenreId,
                        principalTable: "genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_Games_Genres_idx",
                table: "games",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "fk_Top15InterestingGames_Genres1_idx",
                table: "top15interestinggames",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "top10populargames");

            migrationBuilder.DropTable(
                name: "top15interestinggames");

            migrationBuilder.DropTable(
                name: "genres");
        }
    }
}
