using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alexicon.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "GamePlayer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerUsername = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentRackForDb = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentRack = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePlayer_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlayer_Player_PlayerUsername",
                        column: x => x.PlayerUsername,
                        principalTable: "Player",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameMove",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LocationX = table.Column<short>(type: "INTEGER", nullable: false),
                    LocationY = table.Column<short>(type: "INTEGER", nullable: false),
                    Score = table.Column<short>(type: "INTEGER", nullable: false),
                    LettersUsedForDb = table.Column<string>(type: "TEXT", nullable: false),
                    WordsCreatedForDb = table.Column<string>(type: "TEXT", nullable: false),
                    LettersUsed = table.Column<string>(type: "TEXT", nullable: false),
                    WordsCreated = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameMove_GamePlayer_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "GamePlayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameMove_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMove_GameId",
                table: "GameMove",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameMove_PlayerId",
                table: "GameMove",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayer_GameId",
                table: "GamePlayer",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayer_PlayerUsername",
                table: "GamePlayer",
                column: "PlayerUsername");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMove");

            migrationBuilder.DropTable(
                name: "GamePlayer");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
