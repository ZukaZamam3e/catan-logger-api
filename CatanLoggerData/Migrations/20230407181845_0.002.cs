using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatanLoggerData.Migrations
{
    /// <inheritdoc />
    public partial class _0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GAME_ID",
                table: "CL_DICEROLL",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GAME_ID",
                table: "CL_DICEROLL");
        }
    }
}
