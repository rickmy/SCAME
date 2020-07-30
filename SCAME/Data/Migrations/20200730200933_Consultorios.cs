using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Data.Migrations
{
    public partial class Consultorios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultorio_AspNetUsers_AspNetUser",
                table: "Consultorio");

            migrationBuilder.DropIndex(
                name: "IX_Consultorio_AspNetUser",
                table: "Consultorio");

            migrationBuilder.DropColumn(
                name: "AspNetUser",
                table: "Consultorio");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Consultorio",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultorio_UserId",
                table: "Consultorio",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultorio_AspNetUsers_UserId",
                table: "Consultorio",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultorio_AspNetUsers_UserId",
                table: "Consultorio");

            migrationBuilder.DropIndex(
                name: "IX_Consultorio_UserId",
                table: "Consultorio");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Consultorio");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUser",
                table: "Consultorio",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultorio_AspNetUser",
                table: "Consultorio",
                column: "AspNetUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultorio_AspNetUsers_AspNetUser",
                table: "Consultorio",
                column: "AspNetUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
