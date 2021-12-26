using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Admegatest.Data.Migrations
{
    public partial class UserNameField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Email");
        }
    }
}
