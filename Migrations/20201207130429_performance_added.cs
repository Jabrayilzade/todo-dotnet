using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoWish.Migrations
{
    public partial class performance_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Performance",
                table: "statistics",
                newName: "performance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "performance",
                table: "statistics",
                newName: "Performance");
        }
    }
}
