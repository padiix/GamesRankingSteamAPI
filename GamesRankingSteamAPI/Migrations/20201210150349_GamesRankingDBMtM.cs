using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace GamesRankingSteamAPI.Migrations
{
    public partial class GamesRankingDBMtM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_genres_games",
                table: "genres");

            migrationBuilder.DropForeignKey(
                name: "fk_genres_top15interestinggames1",
                table: "genres");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropIndex(
                name: "fk_genres_games_idx",
                table: "genres");

            migrationBuilder.DropIndex(
                name: "fk_genres_top15interestinggames1_idx",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "games_GameId",
                table: "genres");

            migrationBuilder.DropColumn(
                name: "top15interestinggames_GameId",
                table: "genres");

            migrationBuilder.CreateTable(
                name: "top15interestinggames_has_genres",
                columns: table => new
                {
                    top15interestinggames_GameId = table.Column<long>(nullable: false),
                    genres_GenreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.top15interestinggames_GameId, x.genres_GenreId });
                    table.ForeignKey(
                        name: "fk_top15interestinggames_has_genres_genres1",
                        column: x => x.genres_GenreId,
                        principalTable: "genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_top15interestinggames_has_genres_top15interestinggames",
                        column: x => x.top15interestinggames_GameId,
                        principalTable: "top15interestinggames",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fk_top15interestinggames_has_genres_genres1_idx",
                table: "top15interestinggames_has_genres",
                column: "genres_GenreId");

            migrationBuilder.CreateIndex(
                name: "fk_top15interestinggames_has_genres_top15interestinggames_idx",
                table: "top15interestinggames_has_genres",
                column: "top15interestinggames_GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "top15interestinggames_has_genres");

            migrationBuilder.AddColumn<long>(
                name: "games_GameId",
                table: "genres",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "top15interestinggames_GameId",
                table: "genres",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    GameId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PEGI = table.Column<int>(type: "int", nullable: true),
                    Summary = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    Title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    URL = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.GameId);
                });

            migrationBuilder.CreateIndex(
                name: "fk_genres_games_idx",
                table: "genres",
                column: "games_GameId");

            migrationBuilder.CreateIndex(
                name: "fk_genres_top15interestinggames1_idx",
                table: "genres",
                column: "top15interestinggames_GameId");

            migrationBuilder.AddForeignKey(
                name: "fk_genres_games",
                table: "genres",
                column: "games_GameId",
                principalTable: "games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_genres_top15interestinggames1",
                table: "genres",
                column: "top15interestinggames_GameId",
                principalTable: "top15interestinggames",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
