using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Menu_Migration_Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Menu_RootMenuId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_RootMenuId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "RootMenuId",
                table: "Menu");

            migrationBuilder.AlterColumn<int>(
                name: "RootId",
                table: "Menu",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Menu_RootId",
                table: "Menu",
                column: "RootId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Menu_RootId",
                table: "Menu",
                column: "RootId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Menu_RootId",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_RootId",
                table: "Menu");

            migrationBuilder.AlterColumn<int>(
                name: "RootId",
                table: "Menu",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RootMenuId",
                table: "Menu",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menu_RootMenuId",
                table: "Menu",
                column: "RootMenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Menu_RootMenuId",
                table: "Menu",
                column: "RootMenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
