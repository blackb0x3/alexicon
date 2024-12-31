using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alexicon.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_ValidateNewWords_To_Game : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ValidateNewWords",
                table: "Games",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidateNewWords",
                table: "Games");
        }
    }
}
