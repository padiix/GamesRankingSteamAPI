using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamesRankingSteamAPI.Migrations
{
    public partial class GamesRankingDBv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstReleaseDate",
                table: "top10populargames");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "top10populargames");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "games",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "games",
                maxLength: 1000,
                nullable: true,
                comment: "opis - opis gry video (kopiuj wklej z strony internetowej zawierającej informacje o grze)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "games");

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstReleaseDate",
                table: "top10populargames",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "top10populargames",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "opis - opis gry video (kopiuj wklej z strony internetowej zawierającej informacje o grze)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "games",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);
        }
    }
}
