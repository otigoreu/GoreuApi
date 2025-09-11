using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_fechaAnulacion_field_USUARIOUNIDADORGANICA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAnulacion",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAnulacion",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica");
        }
    }
}
