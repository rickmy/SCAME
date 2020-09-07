using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Migrations
{
    public partial class Paciente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discapacidad",
                columns: table => new
                {
                    IdDiscapacidad = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDiscapacidad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discapacidad", x => x.IdDiscapacidad);
                });

            migrationBuilder.CreateTable(
                name: "HorasAtencion",
                columns: table => new
                {
                    IdHorasAtencion = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoraInicio = table.Column<string>(nullable: true),
                    HoraCierre = table.Column<string>(nullable: true),
                    Disponibilidad = table.Column<bool>(nullable: false),
                    Estado = table.Column<bool>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasAtencion", x => x.IdHorasAtencion);
                    table.ForeignKey(
                        name: "FK_HorasAtencion_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(nullable: true),
                    PrimerNombre = table.Column<string>(nullable: true),
                    SegundoNombre = table.Column<string>(nullable: true),
                    PrimerApellido = table.Column<string>(nullable: true),
                    SegundoApellido = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    PruebaCovid = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    CasoConfirmado = table.Column<bool>(nullable: false),
                    NumeroCarnet = table.Column<string>(nullable: true),
                    Porcentaje = table.Column<string>(nullable: true),
                    DiscapacidadId = table.Column<int>(nullable: false),
                    CantonId = table.Column<int>(nullable: false),
                    SexoId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.IdPaciente);
                    table.ForeignKey(
                        name: "FK_Paciente_Canton_CantonId",
                        column: x => x.CantonId,
                        principalTable: "Canton",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paciente_Discapacidad_DiscapacidadId",
                        column: x => x.DiscapacidadId,
                        principalTable: "Discapacidad",
                        principalColumn: "IdDiscapacidad",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paciente_Sexos_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexos",
                        principalColumn: "IdSexo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paciente_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HorasAtencion_TurnoId",
                table: "HorasAtencion",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_CantonId",
                table: "Paciente",
                column: "CantonId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_DiscapacidadId",
                table: "Paciente",
                column: "DiscapacidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_SexoId",
                table: "Paciente",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_UserId",
                table: "Paciente",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorasAtencion");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Discapacidad");
        }
    }
}
