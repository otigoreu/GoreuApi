using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioRolMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_Rol_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_EntidadAplicacionRol_Rol_IdRol",
                schema: "Administrador",
                table: "EntidadAplicacionRol");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuRol_Rol_IdRol",
                schema: "Administrador",
                table: "MenuRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                schema: "Administrador",
                table: "Rol");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "Administrador",
                table: "Rol");

            migrationBuilder.DropColumn(
                name: "Estado",
                schema: "Administrador",
                table: "Rol");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                schema: "Administrador",
                table: "Rol",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldUnicode: false,
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Administrador",
                table: "Rol",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldUnicode: false,
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadAplicacionRol_AspNetRoles_IdRol",
                schema: "Administrador",
                table: "EntidadAplicacionRol",
                column: "IdRol",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuRol_AspNetRoles_IdRol",
                schema: "Administrador",
                table: "MenuRol",
                column: "IdRol",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_AspNetRoles_RoleId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_EntidadAplicacionRol_AspNetRoles_IdRol",
                schema: "Administrador",
                table: "EntidadAplicacionRol");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuRol_AspNetRoles_IdRol",
                schema: "Administrador",
                table: "MenuRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_AspNetRoles_RoleId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                schema: "Administrador",
                table: "Rol",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Administrador",
                table: "Rol",
                type: "varchar(256)",
                unicode: false,
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "Administrador",
                table: "Rol",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                schema: "Administrador",
                table: "Rol",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Administrador",
                table: "Rol",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Rol_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "Administrador",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntidadAplicacionRol_Rol_IdRol",
                schema: "Administrador",
                table: "EntidadAplicacionRol",
                column: "IdRol",
                principalSchema: "Administrador",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuRol_Rol_IdRol",
                schema: "Administrador",
                table: "MenuRol",
                column: "IdRol",
                principalSchema: "Administrador",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Rol_RoleId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "RoleId",
                principalSchema: "Administrador",
                principalTable: "Rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
