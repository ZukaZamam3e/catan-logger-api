using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatanLoggerData.Migrations
{
    /// <inheritdoc />
    public partial class _0001Inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CL_DICEROLL",
                columns: table => new
                {
                    DICE_ROLL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DICE_NUMBER = table.Column<int>(type: "int", nullable: false),
                    DICE_ROLLS = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_DICEROLL", x => x.DICE_ROLL_ID);
                });

            migrationBuilder.CreateTable(
                name: "CL_GAME",
                columns: table => new
                {
                    GAME_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    DATE_PLAYED = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_GAME", x => x.GAME_ID);
                });

            migrationBuilder.CreateTable(
                name: "CL_PLAYER",
                columns: table => new
                {
                    PLAYER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GAME_ID = table.Column<int>(type: "int", nullable: false),
                    PLAYER_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PLAYER_COLOR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TURN_ORDER = table.Column<int>(type: "int", nullable: false),
                    WINNER = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_PLAYER", x => x.PLAYER_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CL_DICEROLL");

            migrationBuilder.DropTable(
                name: "CL_GAME");

            migrationBuilder.DropTable(
                name: "CL_PLAYER");
        }
    }
}
