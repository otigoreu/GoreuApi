using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Rows_table_UserRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuario_UserId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolInfo",
                table: "RolInfo");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                schema: "Administrador",
                table: "UsuarioRol",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "Administrador",
                table: "UsuarioRol",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "RolInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RolEntidadAplicacionInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEntidadAplicacion = table.Column<int>(type: "int", nullable: false),
                    EntidadAplicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UserId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "RoleId",
                principalSchema: "Administrador",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuario_UserId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "UserId",
                principalSchema: "Administrador",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuario_UserId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "RolEntidadAplicacionInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_UserId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                schema: "Administrador",
                table: "UsuarioRol",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "RolInfo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolInfo",
                table: "RolInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "RoleId",
                principalSchema: "Administrador",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuario_UserId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "UserId",
                principalSchema: "Administrador",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
