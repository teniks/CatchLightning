using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchLightning.Migrations
{
    /// <inheritdoc />
    public partial class RenameAchievementTitleToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Achivments",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Achivments",
                newName: "Title");
        }
    }
}
