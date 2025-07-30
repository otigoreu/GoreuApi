using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class added_UsuarioRoles_field_USUARIO_ROL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RolId",
                schema: "Administrador",
                table: "UsuarioRol",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                schema: "Administrador",
                table: "UsuarioRol",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuarioId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Rol_RolId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "RolId",
                principalSchema: "Administrador",
                principalTable: "Rol",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuario_UsuarioId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "UsuarioId",
                principalSchema: "Administrador",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Rol_RolId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuario_UsuarioId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_RolId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_UsuarioId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "RolId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                schema: "Administrador",
                table: "UsuarioRol");
        }
    }
}
