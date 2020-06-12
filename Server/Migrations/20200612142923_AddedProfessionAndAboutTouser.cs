using Microsoft.EntityFrameworkCore.Migrations;

namespace Onebrb.Server.Migrations
{
    public partial class AddedProfessionAndAboutTouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Users");
        }
    }
}
