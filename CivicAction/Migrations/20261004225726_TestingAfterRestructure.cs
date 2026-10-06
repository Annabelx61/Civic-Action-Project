using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicAction.Migrations
{
    /// <inheritdoc />
    public partial class TestingAfterRestructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Account_StudentID",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Update_Project_ProjectID",
                table: "Update");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Account_AdminID",
                table: "Verification");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Project_ProjectID",
                table: "Verification");

            migrationBuilder.DropTable(
                name: "VolunteerHour");

            migrationBuilder.DropTable(
                name: "VolunteerOrganization");

            migrationBuilder.DropIndex(
                name: "IX_Project_StudentID",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsWorkshop",
                table: "Update");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Update");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Verification",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "AdminID",
                table: "Verification",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Verification_ProjectID",
                table: "Verification",
                newName: "IX_Verification_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Verification_AdminID",
                table: "Verification",
                newName: "IX_Verification_AdminId");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "Update",
                newName: "VerificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Update_ProjectID",
                table: "Update",
                newName: "IX_Update_VerificationId");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Project",
                newName: "SiteLocation");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Verification",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "Verification",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Verification",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "AdminId",
                table: "Verification",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Verification",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Organization",
                table: "Project",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "School",
                table: "Account",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Account",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_StudentId",
                table: "Verification",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Verification_VerificationId",
                table: "Update",
                column: "VerificationId",
                principalTable: "Verification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Account_AdminId",
                table: "Verification",
                column: "AdminId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Account_StudentId",
                table: "Verification",
                column: "StudentId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Project_ProjectId",
                table: "Verification",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Update_Verification_VerificationId",
                table: "Update");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Account_AdminId",
                table: "Verification");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Account_StudentId",
                table: "Verification");

            migrationBuilder.DropForeignKey(
                name: "FK_Verification_Project_ProjectId",
                table: "Verification");

            migrationBuilder.DropIndex(
                name: "IX_Verification_StudentId",
                table: "Verification");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Verification");

            migrationBuilder.DropColumn(
                name: "School",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Verification",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Verification",
                newName: "AdminID");

            migrationBuilder.RenameIndex(
                name: "IX_Verification_ProjectId",
                table: "Verification",
                newName: "IX_Verification_ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Verification_AdminId",
                table: "Verification",
                newName: "IX_Verification_AdminID");

            migrationBuilder.RenameColumn(
                name: "VerificationId",
                table: "Update",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Update_VerificationId",
                table: "Update",
                newName: "IX_Update_ProjectID");

            migrationBuilder.RenameColumn(
                name: "SiteLocation",
                table: "Project",
                newName: "StudentID");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectID",
                table: "Verification",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                table: "Verification",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Feedback",
                table: "Verification",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdminID",
                table: "Verification",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWorkshop",
                table: "Update",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StudentID",
                table: "Update",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Organization",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "School",
                table: "Account",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Account",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VolunteerOrganization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerOrganization_Account_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerHour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VolunteerOrganizationID = table.Column<int>(type: "INTEGER", nullable: false),
                    Hours = table.Column<double>(type: "REAL", nullable: false),
                    WorkDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WorkDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerHour_VolunteerOrganization_VolunteerOrganizationID",
                        column: x => x.VolunteerOrganizationID,
                        principalTable: "VolunteerOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_StudentID",
                table: "Project",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerHour_VolunteerOrganizationID",
                table: "VolunteerHour",
                column: "VolunteerOrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerOrganization_StudentID",
                table: "VolunteerOrganization",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Account_StudentID",
                table: "Project",
                column: "StudentID",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Update_Project_ProjectID",
                table: "Update",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Account_AdminID",
                table: "Verification",
                column: "AdminID",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Verification_Project_ProjectID",
                table: "Verification",
                column: "ProjectID",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
