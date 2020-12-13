using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace GamesRankingSteamAPI.Migrations
{
    public partial class GamesRankingDBv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    GameId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    PEGI = table.Column<int>(nullable: true),
                    URL = table.Column<string>(maxLength: 150, nullable: false),
                    Summary = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "top10populargames",
                columns: table => new
                {
                    GameId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Rating = table.Column<double>(nullable: true),
                    RatingCount = table.Column<int>(nullable: true),
                    Updated = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "top15interestinggames",
                columns: table => new
                {
                    GameId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    FirstReleaseDate = table.Column<DateTime>(type: "date", nullable: true),
                    URL = table.Column<string>(maxLength: 150, nullable: true),
                    Updated = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GameId);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    GenreId = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    games_GameId = table.Column<long>(nullable: true),
                    top15interestinggames_GameId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GenreId);
                    table.ForeignKey(
                        name: "fk_genres_games",
                        column: x => x.games_GameId,
                        principalTable: "games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_genres_top15interestinggames1",
                        column: x => x.top15interestinggames_GameId,
                        principalTable: "top15interestinggames",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_genres_games_idx",
                table: "genres",
                column: "games_GameId");

            migrationBuilder.CreateIndex(
                name: "fk_genres_top15interestinggames1_idx",
                table: "genres",
                column: "top15interestinggames_GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "top10populargames");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "top15interestinggames");
        }
    }
}
