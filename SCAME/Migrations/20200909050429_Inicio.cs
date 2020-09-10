using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SCAME.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "Especialidad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEspecialidad = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePais = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProvincia = table.Column<string>(nullable: true),
                    PaisId = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provincia_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Canton",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCanton = table.Column<string>(nullable: true),
                    ProvinciaId = table.Column<int>(nullable: false),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canton", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canton_Provincia_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultorio",
                columns: table => new
                {
                    IdConsultorio = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Ruc = table.Column<string>(nullable: true),
                    NombreConsultorio = table.Column<string>(nullable: true),
                    CedulaRepresentanteLegal = table.Column<string>(nullable: true),
                    NombreRepresentateLegal = table.Column<string>(nullable: true),
                    ApellidoRepresentanteLegal = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    NumPatenteMunicipal = table.Column<string>(nullable: true),
                    PermisoFuncionamientoMsp = table.Column<string>(nullable: true),
                    CantonId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultorio", x => x.IdConsultorio);
                    table.ForeignKey(
                        name: "FK_Consultorio_Canton_CantonId",
                        column: x => x.CantonId,
                        principalTable: "Canton",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultorio_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<string>(nullable: true),
                    HoraApertura = table.Column<string>(nullable: true),
                    HoraCierre = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    ConsultorioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horario_Consultorio_ConsultorioId",
                        column: x => x.ConsultorioId,
                        principalTable: "Consultorio",
                        principalColumn: "IdConsultorio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CodigoSenecyt = table.Column<string>(nullable: true),
                    TituloEgresado = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    ConsultorioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicos_Consultorio_ConsultorioId",
                        column: x => x.ConsultorioId,
                        principalTable: "Consultorio",
                        principalColumn: "IdConsultorio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTurno = table.Column<string>(nullable: true),
                    DiasTurno = table.Column<string>(nullable: true),
                    HoraInicio = table.Column<string>(nullable: true),
                    HoraSalida = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    ConsultorioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turno_Consultorio_ConsultorioId",
                        column: x => x.ConsultorioId,
                        principalTable: "Consultorio",
                        principalColumn: "IdConsultorio",
                        onDelete: ReferentialAction.Cascade);
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
                    ConsultorioId = table.Column<int>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicoDetalle", x => x.IdMedicoDetalle);
                    table.ForeignKey(
                        name: "FK_MedicoDetalle_Consultorio_ConsultorioId",
                        column: x => x.ConsultorioId,
                        principalTable: "Consultorio",
                        principalColumn: "IdConsultorio",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "Cita",
                columns: table => new
                {
                    IdCita = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacienteId = table.Column<int>(nullable: false),
                    FechaCita = table.Column<DateTime>(nullable: false),
                    MedicoId = table.Column<int>(nullable: false),
                    MotivoCita = table.Column<string>(nullable: true),
                    MotivoAnulacion = table.Column<string>(nullable: true),
                    EspecialidadId = table.Column<int>(nullable: false),
                    ConsultorioId = table.Column<int>(nullable: false),
                    HorasAtencionId = table.Column<int>(nullable: false),
                    EstadoCita = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cita", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Cita_Consultorio_ConsultorioId",
                        column: x => x.ConsultorioId,
                        principalTable: "Consultorio",
                        principalColumn: "IdConsultorio",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cita_Especialidad_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cita_HorasAtencion_HorasAtencionId",
                        column: x => x.HorasAtencionId,
                        principalTable: "HorasAtencion",
                        principalColumn: "IdHorasAtencion",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cita_Medicos_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cita_Paciente_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Paciente",
                        principalColumn: "IdPaciente",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Canton_ProvinciaId",
                table: "Canton",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_ConsultorioId",
                table: "Cita",
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
                name: "IX_Cita_MedicoId",
                table: "Cita",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_PacienteId",
                table: "Cita",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultorio_CantonId",
                table: "Consultorio",
                column: "CantonId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultorio_UserId",
                table: "Consultorio",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Horario_ConsultorioId",
                table: "Horario",
                column: "ConsultorioId");

            migrationBuilder.CreateIndex(
                name: "IX_HorasAtencion_TurnoId",
                table: "HorasAtencion",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_ConsultorioId",
                table: "MedicoDetalle",
                column: "ConsultorioId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_EspecialidadId",
                table: "MedicoDetalle",
                column: "EspecialidadId",
                filter: "[EspecialidadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_MedicoId",
                table: "MedicoDetalle",
                column: "MedicoId",
                filter: "[MedicoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MedicoDetalle_TurnoId",
                table: "MedicoDetalle",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_ConsultorioId",
                table: "Medicos",
                column: "ConsultorioId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Provincia_PaisId",
                table: "Provincia",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Turno_ConsultorioId",
                table: "Turno",
                column: "ConsultorioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "MedicoDetalle");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "HorasAtencion");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Especialidad");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Discapacidad");

            migrationBuilder.DropTable(
                name: "Sexos");

            migrationBuilder.DropTable(
                name: "Consultorio");

            migrationBuilder.DropTable(
                name: "Canton");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
