using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatanLoggerData.Migrations
{
    /// <inheritdoc />
    public partial class _0003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CL_PLAYER_GAME_ID",
                table: "CL_PLAYER",
                column: "GAME_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CL_DICEROLL_GAME_ID",
                table: "CL_DICEROLL",
                column: "GAME_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CL_DICEROLL_CL_GAME_GAME_ID",
                table: "CL_DICEROLL",
                column: "GAME_ID",
                principalTable: "CL_GAME",
                principalColumn: "GAME_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CL_PLAYER_CL_GAME_GAME_ID",
                table: "CL_PLAYER",
                column: "GAME_ID",
                principalTable: "CL_GAME",
                principalColumn: "GAME_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CL_DICEROLL_CL_GAME_GAME_ID",
                table: "CL_DICEROLL");

            migrationBuilder.DropForeignKey(
                name: "FK_CL_PLAYER_CL_GAME_GAME_ID",
                table: "CL_PLAYER");

            migrationBuilder.DropIndex(
                name: "IX_CL_PLAYER_GAME_ID",
                table: "CL_PLAYER");

            migrationBuilder.DropIndex(
                name: "IX_CL_DICEROLL_GAME_ID",
                table: "CL_DICEROLL");
        }
    }
}
