using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_ControlVigencia_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Desde",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Hasta",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desde",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica");

            migrationBuilder.DropColumn(
                name: "Hasta",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica");
        }
    }
}
