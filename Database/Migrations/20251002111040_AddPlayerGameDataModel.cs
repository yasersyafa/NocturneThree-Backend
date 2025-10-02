using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerGameDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameData_Games_GameId",
                table: "GameData");

            migrationBuilder.DropForeignKey(
                name: "FK_GameData_Players_PlayerId",
                table: "GameData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameData",
                table: "GameData");

            migrationBuilder.RenameTable(
                name: "GameData",
                newName: "PlayerGameData");

            migrationBuilder.RenameIndex(
                name: "IX_GameData_UserId_GameId",
                table: "PlayerGameData",
                newName: "IX_PlayerGameData_UserId_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameData_PlayerId",
                table: "PlayerGameData",
                newName: "IX_PlayerGameData_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameData_GameId",
                table: "PlayerGameData",
                newName: "IX_PlayerGameData_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGameData",
                table: "PlayerGameData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGameData_Games_GameId",
                table: "PlayerGameData",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerGameData_Players_PlayerId",
                table: "PlayerGameData",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGameData_Games_GameId",
                table: "PlayerGameData");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerGameData_Players_PlayerId",
                table: "PlayerGameData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGameData",
                table: "PlayerGameData");

            migrationBuilder.RenameTable(
                name: "PlayerGameData",
                newName: "GameData");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGameData_UserId_GameId",
                table: "GameData",
                newName: "IX_GameData_UserId_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGameData_PlayerId",
                table: "GameData",
                newName: "IX_GameData_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerGameData_GameId",
                table: "GameData",
                newName: "IX_GameData_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameData",
                table: "GameData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameData_Games_GameId",
                table: "GameData",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameData_Players_PlayerId",
                table: "GameData",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
