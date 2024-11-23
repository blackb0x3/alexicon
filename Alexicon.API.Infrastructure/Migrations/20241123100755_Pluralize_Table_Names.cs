using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alexicon.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Pluralize_Table_Names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMove_GamePlayer_PlayerId",
                table: "GameMove");

            migrationBuilder.DropForeignKey(
                name: "FK_GameMove_Games_GameId",
                table: "GameMove");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayer_Games_GameId",
                table: "GamePlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayer_Player_PlayerUsername",
                table: "GamePlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlayer",
                table: "GamePlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameMove",
                table: "GameMove");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "GamePlayer",
                newName: "GamePlayers");

            migrationBuilder.RenameTable(
                name: "GameMove",
                newName: "GameMoves");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayer_PlayerUsername",
                table: "GamePlayers",
                newName: "IX_GamePlayers_PlayerUsername");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayer_GameId",
                table: "GamePlayers",
                newName: "IX_GamePlayers_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameMove_PlayerId",
                table: "GameMoves",
                newName: "IX_GameMoves_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameMove_GameId",
                table: "GameMoves",
                newName: "IX_GameMoves_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlayers",
                table: "GamePlayers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameMoves",
                table: "GameMoves",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMoves_GamePlayers_PlayerId",
                table: "GameMoves",
                column: "PlayerId",
                principalTable: "GamePlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameMoves_Games_GameId",
                table: "GameMoves",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayers_Games_GameId",
                table: "GamePlayers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayers_Players_PlayerUsername",
                table: "GamePlayers",
                column: "PlayerUsername",
                principalTable: "Players",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameMoves_GamePlayers_PlayerId",
                table: "GameMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_GameMoves_Games_GameId",
                table: "GameMoves");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayers_Games_GameId",
                table: "GamePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayers_Players_PlayerUsername",
                table: "GamePlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlayers",
                table: "GamePlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameMoves",
                table: "GameMoves");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "GamePlayers",
                newName: "GamePlayer");

            migrationBuilder.RenameTable(
                name: "GameMoves",
                newName: "GameMove");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayers_PlayerUsername",
                table: "GamePlayer",
                newName: "IX_GamePlayer_PlayerUsername");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlayers_GameId",
                table: "GamePlayer",
                newName: "IX_GamePlayer_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameMoves_PlayerId",
                table: "GameMove",
                newName: "IX_GameMove_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameMoves_GameId",
                table: "GameMove",
                newName: "IX_GameMove_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlayer",
                table: "GamePlayer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameMove",
                table: "GameMove",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameMove_GamePlayer_PlayerId",
                table: "GameMove",
                column: "PlayerId",
                principalTable: "GamePlayer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameMove_Games_GameId",
                table: "GameMove",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayer_Games_GameId",
                table: "GamePlayer",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayer_Player_PlayerUsername",
                table: "GamePlayer",
                column: "PlayerUsername",
                principalTable: "Player",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
