using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Migrations
{
    public partial class Sexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Paciente_PacienteId",
                table: "Cita");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropIndex(
                name: "IX_Cita_PacienteId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Cita");

            migrationBuilder.CreateTable(
                name: "Sexos",
                columns: table => new
                {
                    IdSexo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSexo = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexos", x => x.IdSexo);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sexos");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Cita",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cita_PacienteId",
                table: "Cita",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Paciente_PacienteId",
                table: "Cita",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
