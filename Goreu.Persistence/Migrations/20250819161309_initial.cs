using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Goreu.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Administrador");

            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.CreateTable(
                name: "Aplicacion",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AplicacionInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Entidad",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ruc = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntidadInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "IndiceTabla",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndiceTabla", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuInfoRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAplicacion = table.Column<int>(type: "int", nullable: false),
                    Aplicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    IdMenuPadre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "RolInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Abrev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadOrganicaInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEntidad = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Icono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ruta = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdAplicacion = table.Column<int>(type: "int", nullable: false),
                    IdMenuPadre = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Aplicacion_IdAplicacion",
                        column: x => x.IdAplicacion,
                        principalSchema: "Administrador",
                        principalTable: "Aplicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_IdMenuPadre",
                        column: x => x.IdMenuPadre,
                        principalSchema: "Administrador",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntidadAplicacion",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEntidad = table.Column<int>(type: "int", nullable: false),
                    IdAplicacion = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntidadAplicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntidadAplicacion_Aplicacion_IdAplicacion",
                        column: x => x.IdAplicacion,
                        principalSchema: "Administrador",
                        principalTable: "Aplicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntidadAplicacion_Entidad_IdEntidad",
                        column: x => x.IdEntidad,
                        principalSchema: "Administrador",
                        principalTable: "Entidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadOrganica",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdEntidad = table.Column<int>(type: "int", nullable: false),
                    IdDependencia = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadOrganica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadOrganica_Entidad_IdEntidad",
                        column: x => x.IdEntidad,
                        principalSchema: "Administrador",
                        principalTable: "Entidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnidadOrganica_UnidadOrganica_IdDependencia",
                        column: x => x.IdDependencia,
                        principalSchema: "Administrador",
                        principalTable: "UnidadOrganica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApellidoPat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApellidoMat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNac = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IdTipoDoc = table.Column<int>(type: "int", nullable: false),
                    NroDoc = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_TipoDocumento_IdTipoDoc",
                        column: x => x.IdTipoDoc,
                        principalSchema: "Administrador",
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: true),
                    Nivel = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    IdEntidadAplicacion = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rol_EntidadAplicacion_IdEntidadAplicacion",
                        column: x => x.IdEntidadAplicacion,
                        principalSchema: "Administrador",
                        principalTable: "EntidadAplicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CredencialReniec",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nuDniUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nuRucUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredencialReniec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CredencialReniec_Persona_PersonaID",
                        column: x => x.PersonaID,
                        principalSchema: "Administrador",
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalSchema: "Administrador",
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Rol_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Administrador",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuRol",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operacion = table.Column<bool>(type: "bit", nullable: false),
                    Consulta = table.Column<bool>(type: "bit", nullable: false),
                    IdMenu = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRol", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuRol_Menu_IdMenu",
                        column: x => x.IdMenu,
                        principalSchema: "Administrador",
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuRol_Rol_IdRol",
                        column: x => x.IdRol,
                        principalSchema: "Administrador",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Administrador",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Administrador",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Administrador",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historial",
                schema: "Seguridad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    operacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_pk = table.Column<int>(type: "int", nullable: false),
                    idIndiceTabla = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historial_IndiceTabla_idIndiceTabla",
                        column: x => x.idIndiceTabla,
                        principalSchema: "Seguridad",
                        principalTable: "IndiceTabla",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historial_Usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalSchema: "Administrador",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                schema: "Administrador",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Rol_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Administrador",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario_UserId",
                        column: x => x.UserId,
                        principalSchema: "Administrador",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioUnidadOrganica",
                schema: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdUnidadOrganica = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioUnidadOrganica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioUnidadOrganica_UnidadOrganica_IdUnidadOrganica",
                        column: x => x.IdUnidadOrganica,
                        principalSchema: "Administrador",
                        principalTable: "UnidadOrganica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioUnidadOrganica_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalSchema: "Administrador",
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CredencialReniec_PersonaID",
                schema: "Administrador",
                table: "CredencialReniec",
                column: "PersonaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntidadAplicacion_IdAplicacion",
                schema: "Administrador",
                table: "EntidadAplicacion",
                column: "IdAplicacion");

            migrationBuilder.CreateIndex(
                name: "IX_EntidadAplicacion_IdEntidad",
                schema: "Administrador",
                table: "EntidadAplicacion",
                column: "IdEntidad");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_idIndiceTabla",
                schema: "Seguridad",
                table: "Historial",
                column: "idIndiceTabla");

            migrationBuilder.CreateIndex(
                name: "IX_Historial_idUsuario",
                schema: "Seguridad",
                table: "Historial",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_IdAplicacion",
                schema: "Administrador",
                table: "Menu",
                column: "IdAplicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_IdMenuPadre",
                schema: "Administrador",
                table: "Menu",
                column: "IdMenuPadre");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRol_IdMenu",
                schema: "Administrador",
                table: "MenuRol",
                column: "IdMenu");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRol_IdRol",
                schema: "Administrador",
                table: "MenuRol",
                column: "IdRol");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdTipoDoc",
                schema: "Administrador",
                table: "Persona",
                column: "IdTipoDoc");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_IdEntidadAplicacion",
                schema: "Administrador",
                table: "Rol",
                column: "IdEntidadAplicacion");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Administrador",
                table: "Rol",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadOrganica_IdDependencia",
                schema: "Administrador",
                table: "UnidadOrganica",
                column: "IdDependencia");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadOrganica_IdEntidad",
                schema: "Administrador",
                table: "UnidadOrganica",
                column: "IdEntidad");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Administrador",
                table: "Usuario",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdPersona",
                schema: "Administrador",
                table: "Usuario",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Administrador",
                table: "Usuario",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RoleId",
                schema: "Administrador",
                table: "UsuarioRol",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioUnidadOrganica_IdUnidadOrganica",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica",
                column: "IdUnidadOrganica");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioUnidadOrganica_IdUsuario",
                schema: "Administrador",
                table: "UsuarioUnidadOrganica",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplicacionInfo");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CredencialReniec",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "EntidadInfo");

            migrationBuilder.DropTable(
                name: "Historial",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "MenuInfoRol");

            migrationBuilder.DropTable(
                name: "MenuRol",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "RolInfo");

            migrationBuilder.DropTable(
                name: "UnidadOrganicaInfo");

            migrationBuilder.DropTable(
                name: "UsuarioRol",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "UsuarioUnidadOrganica",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "IndiceTabla",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "UnidadOrganica",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "EntidadAplicacion",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "Persona",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "Aplicacion",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "Entidad",
                schema: "Administrador");

            migrationBuilder.DropTable(
                name: "TipoDocumento",
                schema: "Administrador");
        }
    }
}
