using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagementSystem.Migrations
{
    public partial class anuj4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRoleId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_UserRoleId",
                table: "MyUsers",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyUsers_AspNetRoles_UserRoleId",
                table: "MyUsers",
                column: "UserRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyUsers_AspNetRoles_UserRoleId",
                table: "MyUsers");

            migrationBuilder.DropIndex(
                name: "IX_MyUsers_UserRoleId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "MyUsers");
        }
    }
}
