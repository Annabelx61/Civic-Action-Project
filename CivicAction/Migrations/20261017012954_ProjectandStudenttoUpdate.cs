using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicAction.Migrations
{
    /// <inheritdoc />
    public partial class ProjectandStudenttoUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Update",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Update",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Update_ProjectId",
                table: "Update",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Update_StudentId",
                table: "Update",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Account_StudentId",
                table: "Update",
                column: "StudentId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Project_ProjectId",
                table: "Update",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Update_Account_StudentId",
                table: "Update");

            migrationBuilder.DropForeignKey(
                name: "FK_Update_Project_ProjectId",
                table: "Update");

            migrationBuilder.DropIndex(
                name: "IX_Update_ProjectId",
                table: "Update");

            migrationBuilder.DropIndex(
                name: "IX_Update_StudentId",
                table: "Update");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Update");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Update");
        }
    }
}
