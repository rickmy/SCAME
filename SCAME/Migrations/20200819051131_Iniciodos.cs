using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Migrations
{
    public partial class Iniciodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultorio_Canton_CantonId",
                table: "Consultorio");

            migrationBuilder.DropIndex(
                name: "IX_Consultorio_CantonId",
                table: "Consultorio");

            migrationBuilder.AddColumn<string>(
                name: "Canton",
                table: "Consultorio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Canton",
                table: "Consultorio");

            migrationBuilder.CreateIndex(
                name: "IX_Consultorio_CantonId",
                table: "Consultorio",
                column: "CantonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultorio_Canton_CantonId",
                table: "Consultorio",
                column: "CantonId",
                principalTable: "Canton",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
