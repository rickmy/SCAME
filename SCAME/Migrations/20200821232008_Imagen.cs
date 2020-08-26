using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Migrations
{
    public partial class Imagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Turno_TurnoId",
                table: "Medicos");

            migrationBuilder.DropTable(
                name: "ConsultorioDetalle");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_TurnoId",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "TurnoId",
                table: "Medicos");

            migrationBuilder.AlterColumn<string>(
                name: "HoraSalida",
                table: "Turno",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "HoraInicio",
                table: "Turno",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DiasTurno",
                table: "Turno",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ConsultorioId",
                table: "Turno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "HoraCierre",
                table: "Horario",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "HoraApertura",
                table: "Horario",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Consultorio",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MedicoDetalle",
                columns: table => new
                {
                    IdMedicoDetalle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(nullable: true),
                    EspecialidadId = table.Column<int>(nullable: true),
                    PrecioEspecialidad = table.Column<double>(nullable: true),
                    DescripcionEspecialidad = table.Column<string>(nullable: true),
                    TurnoId = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoDetalle", x => x.IdMedicoDetalle);
                    table.ForeignKey(
                        name: "FK_MedicoDetalle_Especialidad_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicoDetalle_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedicoDetalle_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turno_ConsultorioId",
                table: "Turno",
                column: "ConsultorioId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_EspecialidadId",
                table: "MedicoDetalle",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_MedicoId",
                table: "MedicoDetalle",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_TurnoId",
                table: "MedicoDetalle",
                column: "TurnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Consultorio_ConsultorioId",
                table: "Turno",
                column: "ConsultorioId",
                principalTable: "Consultorio",
                principalColumn: "IdConsultorio",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Consultorio_ConsultorioId",
                table: "Turno");

            migrationBuilder.DropTable(
                name: "MedicoDetalle");

            migrationBuilder.DropIndex(
                name: "IX_Turno_ConsultorioId",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "ConsultorioId",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Consultorio");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraSalida",
                table: "Turno",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraInicio",
                table: "Turno",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DiasTurno",
                table: "Turno",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TurnoId",
                table: "Medicos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraCierre",
                table: "Horario",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraApertura",
                table: "Horario",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ConsultorioDetalle",
                columns: table => new
                {
                    IdMedicoEspecialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionEspecialidad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EspecialidadId = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: true),
                    PrecioEspecialidad = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultorioDetalle", x => x.IdMedicoEspecialidad);
                    table.ForeignKey(
                        name: "FK_ConsultorioDetalle_Especialidad_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsultorioDetalle_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_TurnoId",
                table: "Medicos",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultorioDetalle_EspecialidadId",
                table: "ConsultorioDetalle",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultorioDetalle_MedicoId",
                table: "ConsultorioDetalle",
                column: "MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Turno_TurnoId",
                table: "Medicos",
                column: "TurnoId",
                principalTable: "Turno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
