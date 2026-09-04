using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchLightning.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    IdCategory = table.Column<int>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Achivments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    IsComplited = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateComplited = table.Column<string>(type: "TEXT", nullable: true),
                    IdGoal = table.Column<int>(type: "INTEGER", nullable: true),
                    GoalId = table.Column<int>(type: "INTEGER", nullable: true),
                    IdCategory = table.Column<int>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achivments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achivments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Achivments_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AchievementAchievement",
                columns: table => new
                {
                    DependentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    СonditionsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementAchievement", x => new { x.DependentsId, x.СonditionsId });
                    table.ForeignKey(
                        name: "FK_AchievementAchievement_Achivments_DependentsId",
                        column: x => x.DependentsId,
                        principalTable: "Achivments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementAchievement_Achivments_СonditionsId",
                        column: x => x.СonditionsId,
                        principalTable: "Achivments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementAchievement_СonditionsId",
                table: "AchievementAchievement",
                column: "СonditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Achivments_CategoryId",
                table: "Achivments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Achivments_GoalId",
                table: "Achivments",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_CategoryId",
                table: "Goals",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementAchievement");

            migrationBuilder.DropTable(
                name: "Achivments");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
