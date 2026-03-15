using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMilitar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregaCampos1PV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuntosAño3",
                table: "Castigos");

            migrationBuilder.DropColumn(
                name: "PuntosAño4",
                table: "Castigos");

            migrationBuilder.DropColumn(
                name: "PuntosAño5",
                table: "Castigos");

            migrationBuilder.AddColumn<bool>(
                name: "EsPierdeSalida",
                table: "Sanciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PuntosAño3Raw",
                table: "Castigos",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "0");

            migrationBuilder.AddColumn<string>(
                name: "PuntosAño4Raw",
                table: "Castigos",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "0");

            migrationBuilder.AddColumn<string>(
                name: "PuntosAño5Raw",
                table: "Castigos",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsPierdeSalida",
                table: "Sanciones");

            migrationBuilder.DropColumn(
                name: "PuntosAño3Raw",
                table: "Castigos");

            migrationBuilder.DropColumn(
                name: "PuntosAño4Raw",
                table: "Castigos");

            migrationBuilder.DropColumn(
                name: "PuntosAño5Raw",
                table: "Castigos");

            migrationBuilder.AddColumn<int>(
                name: "PuntosAño3",
                table: "Castigos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuntosAño4",
                table: "Castigos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PuntosAño5",
                table: "Castigos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
