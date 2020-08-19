using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Migrations
{
    public partial class ConsultorioHorario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultorio_Horario_HorarioId",
                table: "Consultorio");

            migrationBuilder.DropIndex(
                name: "IX_Consultorio_HorarioId",
                table: "Consultorio");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "Consultorio");

            migrationBuilder.AddColumn<int>(
                name: "ConsultorioId",
                table: "Horario",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Horario_ConsultorioId",
                table: "Horario",
                column: "ConsultorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horario_Consultorio_ConsultorioId",
                table: "Horario",
                column: "ConsultorioId",
                principalTable: "Consultorio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horario_Consultorio_ConsultorioId",
                table: "Horario");

            migrationBuilder.DropIndex(
                name: "IX_Horario_ConsultorioId",
                table: "Horario");

            migrationBuilder.DropColumn(
                name: "ConsultorioId",
                table: "Horario");

            migrationBuilder.AddColumn<int>(
                name: "HorarioId",
                table: "Consultorio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Consultorio_HorarioId",
                table: "Consultorio",
                column: "HorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultorio_Horario_HorarioId",
                table: "Consultorio",
                column: "HorarioId",
                principalTable: "Horario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
