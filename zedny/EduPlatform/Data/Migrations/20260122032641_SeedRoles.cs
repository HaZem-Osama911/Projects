using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Security",
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { "1", "Super_Admin", "SUPER_ADMIN", Guid.NewGuid().ToString() },
                    { "2", "Teacher", "TEACHER", Guid.NewGuid().ToString() },
                    { "3", "Student", "STUDENT", Guid.NewGuid().ToString() }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValues: new object[] { "1", "2", "3" });
        }
    }
}
