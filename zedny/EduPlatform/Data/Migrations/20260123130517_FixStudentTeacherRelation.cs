using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentTeacherRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinedAt",
                table: "StudentTeachers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "StudentTeachers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeachers_ApplicationUserId",
                table: "StudentTeachers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeachers_Users_ApplicationUserId",
                table: "StudentTeachers",
                column: "ApplicationUserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeachers_Users_ApplicationUserId",
                table: "StudentTeachers");

            migrationBuilder.DropIndex(
                name: "IX_StudentTeachers_ApplicationUserId",
                table: "StudentTeachers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "StudentTeachers");

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedAt",
                table: "StudentTeachers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
