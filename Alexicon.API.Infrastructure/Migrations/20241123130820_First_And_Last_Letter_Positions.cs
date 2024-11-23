using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alexicon.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class First_And_Last_Letter_Positions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationY",
                table: "GameMoves",
                newName: "LastLetterY");

            migrationBuilder.RenameColumn(
                name: "LocationX",
                table: "GameMoves",
                newName: "LastLetterX");

            migrationBuilder.AddColumn<short>(
                name: "FirstLetterX",
                table: "GameMoves",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "FirstLetterY",
                table: "GameMoves",
                type: "INTEGER",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstLetterX",
                table: "GameMoves");

            migrationBuilder.DropColumn(
                name: "FirstLetterY",
                table: "GameMoves");

            migrationBuilder.RenameColumn(
                name: "LastLetterY",
                table: "GameMoves",
                newName: "LocationY");

            migrationBuilder.RenameColumn(
                name: "LastLetterX",
                table: "GameMoves",
                newName: "LocationX");
        }
    }
}
