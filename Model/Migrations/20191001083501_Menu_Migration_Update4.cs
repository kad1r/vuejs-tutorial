using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Menu_Migration_Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Menu",
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Menu");
        }
    }
}
