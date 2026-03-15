using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMilitar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BimestresConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Bimestre = table.Column<int>(type: "INTEGER", nullable: false),
                    Año = table.Column<int>(type: "INTEGER", nullable: false),
                    NroSemana = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cerrada = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BimestresConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cadetes",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ApellidosNombres = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Año = table.Column<int>(type: "INTEGER", nullable: false),
                    Division = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadetes", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "Castigos",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    PuntosAño3 = table.Column<int>(type: "INTEGER", nullable: false),
                    PuntosAño4 = table.Column<int>(type: "INTEGER", nullable: false),
                    PuntosAño5 = table.Column<int>(type: "INTEGER", nullable: false),
                    Reincidencia = table.Column<int>(type: "INTEGER", nullable: false),
                    Nota = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Castigos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Supervisores",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Grado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ApellidosNombres = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisores", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "ActitudesMilitares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CadeteDNI = table.Column<string>(type: "TEXT", nullable: false),
                    Bimestre = table.Column<int>(type: "INTEGER", nullable: false),
                    AñoAcademico = table.Column<int>(type: "INTEGER", nullable: false),
                    NotaActitud = table.Column<decimal>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActitudesMilitares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActitudesMilitares_Cadetes_CadeteDNI",
                        column: x => x.CadeteDNI,
                        principalTable: "Cadetes",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sanciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CadeteDNI = table.Column<string>(type: "TEXT", nullable: false),
                    SupervisorDNI = table.Column<string>(type: "TEXT", nullable: false),
                    CastigoCodigo = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Observaciones = table.Column<string>(type: "TEXT", nullable: true),
                    PuntosAplicados = table.Column<int>(type: "INTEGER", nullable: false),
                    SemanaBimestre = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sanciones_Cadetes_CadeteDNI",
                        column: x => x.CadeteDNI,
                        principalTable: "Cadetes",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sanciones_Castigos_CastigoCodigo",
                        column: x => x.CastigoCodigo,
                        principalTable: "Castigos",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sanciones_Supervisores_SupervisorDNI",
                        column: x => x.SupervisorDNI,
                        principalTable: "Supervisores",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActitudesMilitares_CadeteDNI_Bimestre_AñoAcademico",
                table: "ActitudesMilitares",
                columns: new[] { "CadeteDNI", "Bimestre", "AñoAcademico" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BimestresConfig_Bimestre_Año_NroSemana",
                table: "BimestresConfig",
                columns: new[] { "Bimestre", "Año", "NroSemana" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_CadeteDNI_SemanaBimestre",
                table: "Sanciones",
                columns: new[] { "CadeteDNI", "SemanaBimestre" });

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_CastigoCodigo",
                table: "Sanciones",
                column: "CastigoCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_Fecha",
                table: "Sanciones",
                column: "Fecha");

            migrationBuilder.CreateIndex(
                name: "IX_Sanciones_SupervisorDNI",
                table: "Sanciones",
                column: "SupervisorDNI");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActitudesMilitares");

            migrationBuilder.DropTable(
                name: "BimestresConfig");

            migrationBuilder.DropTable(
                name: "Sanciones");

            migrationBuilder.DropTable(
                name: "Cadetes");

            migrationBuilder.DropTable(
                name: "Castigos");

            migrationBuilder.DropTable(
                name: "Supervisores");
        }
    }
}
