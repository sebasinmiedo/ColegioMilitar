using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMilitar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregaNombreSemana : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreSemana",
                table: "BimestresConfig",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreSemana",
                table: "BimestresConfig");
        }
    }
}
