using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicAction.Migrations
{
    /// <inheritdoc />
    public partial class WorkshopHoursToProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WorkshopHours",
                table: "Project",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkshopHours",
                table: "Project");
        }
    }
}
