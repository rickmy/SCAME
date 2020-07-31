using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Data.Migrations
{
    public partial class Especialistas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "Especialista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoSenecyt",
                table: "Especialista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegundoApellido",
                table: "Especialista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegundoNombre",
                table: "Especialista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Especialista",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TituloEgresado",
                table: "Especialista",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEspecialidad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "Especialista");

            migrationBuilder.DropColumn(
                name: "CodigoSenecyt",
                table: "Especialista");

            migrationBuilder.DropColumn(
                name: "SegundoApellido",
                table: "Especialista");

            migrationBuilder.DropColumn(
                name: "SegundoNombre",
                table: "Especialista");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Especialista");

            migrationBuilder.DropColumn(
                name: "TituloEgresado",
                table: "Especialista");
        }
    }
}
