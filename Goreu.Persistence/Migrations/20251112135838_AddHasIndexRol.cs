using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHasIndexRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rol_IdEntidadAplicacion",
                schema: "adm",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                schema: "adm",
                table: "Rol");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_IdEntidadAplicacion_NormalizedName",
                schema: "adm",
                table: "Rol",
                columns: new[] { "IdEntidadAplicacion", "NormalizedName" },
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "adm",
                table: "Rol",
                column: "NormalizedName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rol_IdEntidadAplicacion_NormalizedName",
                schema: "adm",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                schema: "adm",
                table: "Rol");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_IdEntidadAplicacion",
                schema: "adm",
                table: "Rol",
                column: "IdEntidadAplicacion");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "adm",
                table: "Rol",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
        }
    }
}
