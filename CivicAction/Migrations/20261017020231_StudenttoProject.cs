using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicAction.Migrations
{
    /// <inheritdoc />
    public partial class StudenttoProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Project",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_StudentId",
                table: "Project",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Account_StudentId",
                table: "Project",
                column: "StudentId",
                principalTable: "Account",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Account_StudentId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_StudentId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Project");
        }
    }
}
