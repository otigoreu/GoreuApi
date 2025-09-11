using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_observacionAnulacion_fiel_USUARIOUNIDADORGANICA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObservacionAnulacion",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservacionAnulacion",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica");
        }
    }
}
