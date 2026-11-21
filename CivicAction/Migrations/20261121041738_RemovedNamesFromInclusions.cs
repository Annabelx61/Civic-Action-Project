using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicAction.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNamesFromInclusions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstMidName",
                table: "Inclusion");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Inclusion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstMidName",
                table: "Inclusion",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Inclusion",
                type: "TEXT",
                nullable: true);
        }
    }
}
