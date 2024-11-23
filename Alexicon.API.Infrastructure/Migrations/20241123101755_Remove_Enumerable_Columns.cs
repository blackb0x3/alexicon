using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alexicon.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Enumerable_Columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentRack",
                table: "GamePlayers");

            migrationBuilder.DropColumn(
                name: "LettersUsed",
                table: "GameMoves");

            migrationBuilder.DropColumn(
                name: "WordsCreated",
                table: "GameMoves");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentRack",
                table: "GamePlayers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LettersUsed",
                table: "GameMoves",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WordsCreated",
                table: "GameMoves",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
