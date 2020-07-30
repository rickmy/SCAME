using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Data.Migrations
{
    public partial class Consultori : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Consultorio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Consultorio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Consultorio");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Consultorio");
        }
    }
}
