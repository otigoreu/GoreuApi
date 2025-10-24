using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Deleted_email_field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_UserId",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "EntidadAplicacion",
                table: "RolEntidadAplicacionInfo");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Administrador",
                table: "Persona");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.CreateTable(
                name: "MenuInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAplicacion = table.Column<int>(type: "int", nullable: false),
                    Aplicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMenuPadre = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolEntidadAplicacionCounterInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEntidadAplicacion = table.Column<int>(type: "int", nullable: false),
                    CantidadMenus = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolEntidadAplicacionCounterInfo", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuInfo");

            migrationBuilder.DropTable(
                name: "RolEntidadAplicacionCounterInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<string>(
                name: "EntidadAplicacion",
                table: "RolEntidadAplicacionInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Administrador",
                table: "Persona",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UserId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "UserId");
        }
    }
}
