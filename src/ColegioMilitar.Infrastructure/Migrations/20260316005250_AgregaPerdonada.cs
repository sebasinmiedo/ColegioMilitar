using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMilitar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregaPerdonada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Perdonada",
                table: "Sanciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Perdonada",
                table: "Sanciones");
        }
    }
}
