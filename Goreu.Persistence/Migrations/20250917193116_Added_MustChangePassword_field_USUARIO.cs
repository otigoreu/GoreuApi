using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_MustChangePassword_field_USUARIO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MustChangePassword",
                schema: "Administrador",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MustChangePassword",
                schema: "Administrador",
                table: "Usuario");
        }
    }
}
