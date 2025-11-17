using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sudoku.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "UserGames",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "Hint",
                table: "UserGames",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hint",
                table: "UserGames");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "UserGames",
                newName: "State");
        }
    }
}
