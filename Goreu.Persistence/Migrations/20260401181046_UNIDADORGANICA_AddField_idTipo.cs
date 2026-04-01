using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UNIDADORGANICA_AddField_idTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idTipo",
                schema: "adm",
                table: "UnidadOrganica",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idTipo",
                schema: "adm",
                table: "UnidadOrganica");
        }
    }
}
