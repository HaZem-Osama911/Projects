using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaSoft.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        // في SeedRoles Migration
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        INSERT INTO [Security].Roles (Id, Name, NormalizedName, ConcurrencyStamp)
        VALUES 
            (NEWID(), 'ADMIN', 'ADMIN', NEWID()),
            (NEWID(), 'MANAGER', 'MANAGER', NEWID()),
            (NEWID(), 'TECHNICAL_HEAD', 'TECHNICAL_HEAD', NEWID()),
            (NEWID(), 'EMPLOYEE', 'EMPLOYEE', NEWID())
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    "1", "2", "3", "4"
                });
        }
    }
}