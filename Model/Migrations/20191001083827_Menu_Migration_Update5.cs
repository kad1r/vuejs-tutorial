using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class Menu_Migration_Update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Menu_RootId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuAuthorization_Menu_MenuId",
                table: "MenuAuthorization");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuAuthorization_Role_RoleId",
                table: "MenuAuthorization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuAuthorization",
                table: "MenuAuthorization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "MenuAuthorization",
                newName: "MenuAuthorizations");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.RenameIndex(
                name: "IX_MenuAuthorization_RoleId",
                table: "MenuAuthorizations",
                newName: "IX_MenuAuthorizations_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuAuthorization_MenuId",
                table: "MenuAuthorizations",
                newName: "IX_MenuAuthorizations_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_RootId",
                table: "Menus",
                newName: "IX_Menus_RootId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuAuthorizations",
                table: "MenuAuthorizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuAuthorizations_Menus_MenuId",
                table: "MenuAuthorizations",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuAuthorizations_Roles_RoleId",
                table: "MenuAuthorizations",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_RootId",
                table: "Menus",
                column: "RootId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuAuthorizations_Menus_MenuId",
                table: "MenuAuthorizations");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuAuthorizations_Roles_RoleId",
                table: "MenuAuthorizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_RootId",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuAuthorizations",
                table: "MenuAuthorizations");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.RenameTable(
                name: "MenuAuthorizations",
                newName: "MenuAuthorization");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_RootId",
                table: "Menu",
                newName: "IX_Menu_RootId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuAuthorizations_RoleId",
                table: "MenuAuthorization",
                newName: "IX_MenuAuthorization_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuAuthorizations_MenuId",
                table: "MenuAuthorization",
                newName: "IX_MenuAuthorization_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuAuthorization",
                table: "MenuAuthorization",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Menu_RootId",
                table: "Menu",
                column: "RootId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuAuthorization_Menu_MenuId",
                table: "MenuAuthorization",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuAuthorization_Role_RoleId",
                table: "MenuAuthorization",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
