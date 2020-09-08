using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Migrations
{
    public partial class Cita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Medicos_EspecialistaId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Horario_HorarioId",
                table: "Cita");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cita",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_EspecialistaId",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_HorarioId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "Dia",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "EspecialistaId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "NombreConsultorio",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "PrimerApellido",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "PrimerNombre",
                table: "Cita");

            migrationBuilder.AddColumn<int>(
                name: "ConsultorioId",
                table: "MedicoDetalle",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCita",
                table: "Cita",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ConsultorioId",
                table: "Cita",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadId",
                table: "Cita",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Cita",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EstadoCita",
                table: "Cita",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCita",
                table: "Cita",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HorasAtencionId",
                table: "Cita",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MedicoId",
                table: "Cita",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicoId1",
                table: "Cita",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivoAnulacion",
                table: "Cita",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivoCita",
                table: "Cita",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Cita",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cita",
                table: "Cita",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_ConsultorioId",
                table: "MedicoDetalle",
                column: "ConsultorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_EspecialidadId",
                table: "Cita",
                column: "EspecialidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_HorasAtencionId",
                table: "Cita",
                column: "HorasAtencionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_MedicoId1",
                table: "Cita",
                column: "MedicoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_PacienteId",
                table: "Cita",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Especialidad_EspecialidadId",
                table: "Cita",
                column: "EspecialidadId",
                principalTable: "Especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_HorasAtencion_HorasAtencionId",
                table: "Cita",
                column: "HorasAtencionId",
                principalTable: "HorasAtencion",
                principalColumn: "IdHorasAtencion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Medicos_MedicoId1",
                table: "Cita",
                column: "MedicoId1",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Paciente_PacienteId",
                table: "Cita",
                column: "PacienteId",
                principalTable: "Paciente",
                principalColumn: "IdPaciente",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicoDetalle_Consultorio_ConsultorioId",
                table: "MedicoDetalle",
                column: "ConsultorioId",
                principalTable: "Consultorio",
                principalColumn: "IdConsultorio",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Especialidad_EspecialidadId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_HorasAtencion_HorasAtencionId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Medicos_MedicoId1",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_Cita_Paciente_PacienteId",
                table: "Cita");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicoDetalle_Consultorio_ConsultorioId",
                table: "MedicoDetalle");

            migrationBuilder.DropIndex(
                name: "IX_MedicoDetalle_ConsultorioId",
                table: "MedicoDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cita",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_EspecialidadId",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_HorasAtencionId",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_MedicoId1",
                table: "Cita");

            migrationBuilder.DropIndex(
                name: "IX_Cita_PacienteId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "ConsultorioId",
                table: "MedicoDetalle");

            migrationBuilder.DropColumn(
                name: "IdCita",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "ConsultorioId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "EspecialidadId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "EstadoCita",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "FechaCita",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "HorasAtencionId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "MedicoId1",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "MotivoAnulacion",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "MotivoCita",
                table: "Cita");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Cita");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Cita",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dia",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EspecialistaId",
                table: "Cita",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Cita",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HorarioId",
                table: "Cita",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NombreConsultorio",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimerApellido",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimerNombre",
                table: "Cita",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cita",
                table: "Cita",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_EspecialistaId",
                table: "Cita",
                column: "EspecialistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_HorarioId",
                table: "Cita",
                column: "HorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Medicos_EspecialistaId",
                table: "Cita",
                column: "EspecialistaId",
                principalTable: "Medicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cita_Horario_HorarioId",
                table: "Cita",
                column: "HorarioId",
                principalTable: "Horario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
