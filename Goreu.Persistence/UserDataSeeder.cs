using Goreu.Entities;
using Goreu.Entities.Pide;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Goreu.Persistence
{
    public class UserDataSeeder
    {
        private readonly IServiceProvider service;
        private readonly ApplicationDbContext context;

        public UserDataSeeder(IServiceProvider service, ApplicationDbContext context)
        {
            this.service = service;
            this.context = context;
            
        }

        public async Task SeedAsync()
        {

            //User repository
            var userManager = service.GetRequiredService<UserManager<Usuario>>();
            //Role repository
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            //Menu Repository

            #region Entidad

            var entidad1 = new Entidad
            {
                Descripcion = "Sistema",
                Ruc = "10101010101"
            };

            var entidad2 = new Entidad
            {
                Descripcion="Goreu",
                Ruc="12345678901"
            };


            var enti1= await context.Set<Entidad>().FirstOrDefaultAsync(x => x.Descripcion== entidad1.Descripcion);
            var enti2 = await context.Set<Entidad>().FirstOrDefaultAsync(x => x.Descripcion == entidad2.Descripcion);

            if (enti1 is null & enti2 is null)
            {


                context.Set<Entidad>().Add(entidad1);
                context.Set<Entidad>().Add(entidad2);

                await context.SaveChangesAsync();
            }
            #endregion


            #region Aplicacion
            var app0 = new Aplicacion
            {
                Descripcion = "SISTEMA"
            };

            var app1 = new Aplicacion
            {
                Descripcion = "TRAMITE"
            };

            var app2 = new Aplicacion
            {
                Descripcion = "PLANILLA"
            };

            var app3 = new Aplicacion
            {
                Descripcion = "SISMORE"
            };
            var api0 = await context.Set<Aplicacion>().FirstOrDefaultAsync(x => x.Descripcion == app0.Descripcion);
            var api1 = await context.Set<Aplicacion>().FirstOrDefaultAsync(x => x.Descripcion == app1.Descripcion);
            var api2 = await context.Set<Aplicacion>().FirstOrDefaultAsync(x => x.Descripcion == app2.Descripcion);
            var api3 = await context.Set<Aplicacion>().FirstOrDefaultAsync(x => x.Descripcion == app3.Descripcion);

            if (api0 is null & api1 is null & api2 is null & api3 is null)
            {
                context.Set<Aplicacion>().Add(app0);
                context.Set<Aplicacion>().Add(app1);
                context.Set<Aplicacion>().Add(app2);
                context.Set<Aplicacion>().Add(app3);

                await context.SaveChangesAsync();
            }

            #endregion

            #region tipoDocu

            var tipodoc1 = new TipoDocumento
            {
                Descripcion = "Documento nacional de Identidad",
                Abrev = "DNI"

            };
            var tipodoc2 = new TipoDocumento
            {
                Descripcion = "Carnet de Extranjeria",
                Abrev = "CEX"

            };
            var item1 = await context.Set<TipoDocumento>().FirstOrDefaultAsync(x => x.Descripcion == tipodoc1.Descripcion);
            var item2 = await context.Set<TipoDocumento>().FirstOrDefaultAsync(x => x.Descripcion == tipodoc2.Descripcion);
            if (item1 is null & item2 is null)
            {


                context.Set<TipoDocumento>().Add(tipodoc1);
                context.Set<TipoDocumento>().Add(tipodoc2);
                await context.SaveChangesAsync();
            }

            #endregion

            #region UnidadOrganica
            var unidadOrganica1 = new UnidadOrganica
            {
                Descripcion = "OTI",
                IdEntidad = entidad2.Id,
                Dependencia = null


            };


            var unidadOrganica2 = new UnidadOrganica
            {
                Descripcion = "Tesoreria",
                IdEntidad = entidad2.Id,
                Dependencia = null


            };

            var uniOr1 = await context.Set<UnidadOrganica>().FirstOrDefaultAsync(x => x.Descripcion == unidadOrganica1.Descripcion);
            var uniOr2 = await context.Set<UnidadOrganica>().FirstOrDefaultAsync(x => x.Descripcion == unidadOrganica2.Descripcion);
            if (uniOr1 is null & uniOr2 is null)
            {


                context.Set<UnidadOrganica>().Add(unidadOrganica1);

                context.Set<UnidadOrganica>().Add(unidadOrganica2);
                await context.SaveChangesAsync();
            }




            #endregion

            #region EntidadApp

            var entidadApp1 = new EntidadAplicacion
            {

                IdEntidad = entidad1.Id,//Sistema
                IdAplicacion = app0.Id//Sistema

            };
            var entidadApp2 = new EntidadAplicacion
            {

                IdEntidad = entidad2.Id,//Goreu
                IdAplicacion = app1.Id//Tramite

            };
            var entidadApp3 = new EntidadAplicacion
            {

                IdEntidad = entidad2.Id,//Goreu
                IdAplicacion = app2.Id//Planilla

            };
            var entidadApp4 = new EntidadAplicacion
            {

                IdEntidad = entidad2.Id,//Goreu
                IdAplicacion = app3.Id//sismore

            };
            var entiApi1 = await context.Set<EntidadAplicacion>().FirstOrDefaultAsync(x => x.Estado == entidadApp1.Estado);

            if (entiApi1 is null)
            {
                context.Set<EntidadAplicacion>().Add(entidadApp1);
                context.Set<EntidadAplicacion>().Add(entidadApp2);
                context.Set<EntidadAplicacion>().Add(entidadApp3);
                context.Set<EntidadAplicacion>().Add(entidadApp4);

                await context.SaveChangesAsync();
            }



            #endregion

            #region Persona

            var persona1 = new Persona
            {
                Nombres = "Edeher Rossetti",
                ApellidoPat = "Ponce",
                ApellidoMat = "Morales",
                FechaNac = new DateTime(1982, 07, 10),
                Email = "edercin@gmail.com",
                IdTipoDoc = tipodoc1.Id,
                NroDoc = "43056714",


            };

            var persona2 = new Persona
            {
                Nombres = "Patricia",
                ApellidoPat = "Lopez",
                ApellidoMat = "Vasquez",
                FechaNac = new DateTime(1990, 05, 31),
                Email = "edercinsoft@gmail.com",
                IdTipoDoc = tipodoc2.Id,
                NroDoc = "46519259",

            };
            var persona3 = new Persona
            {
                Nombres = "Piero Paolo",
                ApellidoPat = "Llenera",
                ApellidoMat = "Lima",
                FechaNac = new DateTime(1985, 02, 01),
                Email = "pp.llerenalima@gmail.com",
                IdTipoDoc = tipodoc1.Id,
                NroDoc = "42928945",

            };


            var per1 = await context.Set<Persona>().FirstOrDefaultAsync(x => x.NroDoc == persona1.NroDoc);
            var per2 = await context.Set<Persona>().FirstOrDefaultAsync(x => x.NroDoc == persona2.NroDoc);
            var per3 = await context.Set<Persona>().FirstOrDefaultAsync(x => x.NroDoc == persona3.NroDoc);
            if (per1 is null & per2 is null& per3 is null)
            {


                // sino existe crear
                context.Set<Persona>().Add(persona1);
                context.Set<Persona>().Add(persona2);
                context.Set<Persona>().Add(persona3);
                await context.SaveChangesAsync();
            }

            #endregion

            #region Roles
            //Creating roles

            var role1 = new Rol
            {
                Name = Constantes.RolSuperAdmin,
                NormalizedName = Constantes.RolSuperAdmin,
                IdEntidadAplicacion=entidadApp1.Id
            };
            
            var role2 = new Rol
            {
                Name = Constantes.RoleAdminGoreuTramite,
                NormalizedName = Constantes.RoleAdminGoreuTramite,
                IdEntidadAplicacion = entidadApp2.Id
            };
            
            var role3 = new Rol
            {
                Name = Constantes.RolAdminGoreuPlanilla,
                NormalizedName = Constantes.RolAdminGoreuPlanilla,
                IdEntidadAplicacion = entidadApp3.Id
            };
            
            var role4 = new Rol
            {
                Name = Constantes.RolUsuarioGoreuTramite,
                NormalizedName = Constantes.RolUsuarioGoreuTramite,
                IdEntidadAplicacion = entidadApp3.Id
            };

            var rol1 = await context.Set<Rol>().FirstOrDefaultAsync(x => x.Name == role1.Name);
            var rol2 = await context.Set<Rol>().FirstOrDefaultAsync(x => x.Name == role2.Name);
            var rol3 = await context.Set<Rol>().FirstOrDefaultAsync(x => x.Name == role3.Name);
            var rol4 = await context.Set<Rol>().FirstOrDefaultAsync(x => x.Name == role4.Name);

            if (rol1 is null & rol2 is null & rol3 is null & rol4 is null)
            {

                context.Set<Rol>().Add(role1);
                context.Set<Rol>().Add(role2);
                context.Set<Rol>().Add(role3);
                context.Set<Rol>().Add(role4);
                await context.SaveChangesAsync();
            }
            #endregion

            #region menu

            var menu1 = new Menu
            {
                Descripcion = "Persona",
                Icono = "users",
                Ruta = "pages/persona",
                IdAplicacion = app0.Id,
                MenuPadre = null,

            };
            var menu2 = new Menu
            {
                
                Descripcion = "Rol",
                Icono = "user-exclamation",
                Ruta = "pages/rol",
                IdAplicacion = app0.Id,
                MenuPadre = null,

            };
            var menu3 = new Menu
            {
                Descripcion = "Usurios",
                Icono = "user-cog",
                Ruta = "pages/user",
                IdAplicacion = app0.Id,
                MenuPadre = null,

            };
            var menu4 = new Menu
            {
                Descripcion = "TipoDocumento",
                Icono = "file-barcode",
                Ruta = "pages/tipo-documento",
                IdAplicacion = app0.Id,
                MenuPadre = null,

            };
            var menu5 = new Menu
            {
                Descripcion = "Menu",
                Icono = "menu-deep",
                Ruta = "pages/menu",
                IdAplicacion = app0.Id,
                MenuPadre = null,

            };
            var menu6= new Menu
            {
                Descripcion = "Aplicacion",
                Icono = "brand-google-play",
                Ruta = "pages/aplicacion",
                IdAplicacion = app0.Id,
                MenuPadre = null,

            };
            var menu7 = new Menu
            {
                Descripcion = "UnidadOrganica",
                Icono = "building-factory-2",
                Ruta = "pages/unidadOrganica",
                IdAplicacion = app0.Id,
                MenuPadre = null,

            };
            

            var men1 = await context.Set<Menu>().FirstOrDefaultAsync(x => x.Descripcion == menu1.Descripcion);
            var men2 = await context.Set<Menu>().FirstOrDefaultAsync(x => x.Descripcion == menu2.Descripcion);
            var men3 = await context.Set<Menu>().FirstOrDefaultAsync(x => x.Descripcion == menu3.Descripcion);
            var men4 = await context.Set<Menu>().FirstOrDefaultAsync(x => x.Descripcion == menu4.Descripcion);
            var men5 = await context.Set<Menu>().FirstOrDefaultAsync(x => x.Descripcion == menu5.Descripcion);
            var men6 = await context.Set<Menu>().FirstOrDefaultAsync(x => x.Descripcion == menu6.Descripcion);
            var men7 = await context.Set<Menu>().FirstOrDefaultAsync(x => x.Descripcion == menu7.Descripcion);

            if (men1 is null & men2 is null & men3 is null & men4 is null & men5 is null & men6 is null & men7 is null) {

                context.Set<Menu>().Add(menu1);
                context.Set<Menu>().Add(menu2);
                context.Set<Menu>().Add(menu3);
                context.Set<Menu>().Add(menu4);
                context.Set<Menu>().Add(menu5);
                context.Set<Menu>().Add(menu6);
                context.Set<Menu>().Add(menu7);
                await context.SaveChangesAsync();
            }



            #endregion

            #region menuRol

            //var rol1 = await roleManager.FindByNameAsync(Constantes.RoleAdmin);
            //var rol2 = await roleManager.FindByNameAsync(Constantes.RolCliente);
            var menuRol1 = new MenuRol
            {

                IdMenu = menu1.Id,
                IdRol = role1.Id
            };
            var menuRol2 = new MenuRol
            {

                IdMenu = menu2.Id,
                IdRol = role1.Id
            };
            var menuRol3 = new MenuRol
            {

                IdMenu = menu3.Id,
                IdRol = role1.Id
            };
            var menuRol4 = new MenuRol
            {

                IdMenu = menu4.Id,
                IdRol = role1.Id
            };
            var menuRol5 = new MenuRol
            {

                IdMenu = menu5.Id,
                IdRol = role1.Id
            };

            var menuRol6 = new MenuRol
            {

                IdMenu = menu6.Id,
                IdRol = role1.Id
            };
            var menuRol7 = new MenuRol
            {

                IdMenu = menu7.Id,
                IdRol = role1.Id
            };
            var menuRol8 = new MenuRol
            {

                IdMenu = menu1.Id,
                IdRol = role2.Id
            };
            var menuRol9 = new MenuRol
            {

                IdMenu = menu2.Id,
                IdRol = role2.Id
            };
            var menuRol10 = new MenuRol
            {

                IdMenu = menu3.Id,
                IdRol = role2.Id
            };

            var menro1 = await context.Set<MenuRol>().FirstOrDefaultAsync(x => x.Estado == menuRol1.Estado);

            if (menro1 is null)
            {
                context.Set<MenuRol>().Add(menuRol1);
                context.Set<MenuRol>().Add(menuRol2);
                context.Set<MenuRol>().Add(menuRol3);
                context.Set<MenuRol>().Add(menuRol4);
                context.Set<MenuRol>().Add(menuRol5);
                context.Set<MenuRol>().Add(menuRol6);
                context.Set<MenuRol>().Add(menuRol7);
                context.Set<MenuRol>().Add(menuRol8);
                context.Set<MenuRol>().Add(menuRol9); 
                context.Set<MenuRol>().Add(menuRol10);
                await context.SaveChangesAsync();
            }
            #endregion


            #region reniec

            var credencialReniec = new CredencialReniec
            {

                nuDniUsuario = "42928945",
                nuRucUsuario = "20393066386",
                password = "42928945**",
                fechaRegistro = new DateTime(2025, 07, 16),
                UsuarioID = new Guid("3FA85F64-5717-4562-B3FC-2C963F66AFA6"),
                PersonaID = persona3.Id,

            };

            var crede = await context.Set<CredencialReniec>().FirstOrDefaultAsync(x => x.nuRucUsuario == credencialReniec.nuRucUsuario);
            if (crede is null )
            {


                context.Set<CredencialReniec>().Add(credencialReniec);
                await context.SaveChangesAsync();
            }


            #endregion

            #region UsuarioAdmin
            //Admin user
            var adminUser = new Usuario()
            {
                UserName = "43056714",
                Email = "edercin@gmail.com",
                EmailConfirmed = true
            };



            #endregion

            #region UsuarioCustomer
            //Customer user
            var customerUser = new Usuario()
            {
                UserName = "46519259",
                Email = "edercinsoft@gmail.com",
                EmailConfirmed = true
            };

            #endregion

            #region UsuarioAdmin2
            //Admin user
            var adminUser2 = new Usuario()
            {
                UserName = "42928945",
                Email = "pp.llerenalima@gmail.com",
                EmailConfirmed = true
            };
            #endregion

            #region UsurioUno
            if (await userManager.FindByEmailAsync("edercin@gmail.com") is null)
            {


                adminUser.IdPersona = persona1.Id;

                var result = await userManager.CreateAsync(adminUser, "Edeher*2024");
                if (result.Succeeded)
                {
                    // Obtenemos el registro del usuario
                    adminUser = await userManager.FindByEmailAsync(adminUser.Email);
                    // Aqui agregamos el Rol de Administrador para el usuario Admin

                    if (adminUser is not null)
                    {

                        await userManager.AddToRoleAsync(adminUser, role1.Name);
                        await userManager.AddToRoleAsync(adminUser, role2.Name);
                        await userManager.AddToRoleAsync(adminUser, role3.Name);
                        await userManager.AddToRoleAsync(adminUser, role4.Name);


                        var usuarioUnidadOrganica1 = new UsuarioUnidadOrganica
                        {

                            IdUsuario = adminUser.Id,
                            IdUnidadOrganica = unidadOrganica1.Id
                        };
                        context.Set<UsuarioUnidadOrganica>().Add(usuarioUnidadOrganica1 );
                        await context.SaveChangesAsync();

                    }
                }
            }
            #endregion

            #region UsuarioDos
            if (await userManager.FindByEmailAsync("edercinsoft@gmail.com") is null)
            {     
                customerUser.IdPersona = persona2.Id;

                var result = await userManager.CreateAsync(customerUser, "Edeher*2025");
                if (result.Succeeded)
                {
                    // Obtenemos el registro del usuario
                    customerUser = await userManager.FindByEmailAsync(customerUser.Email);
                    // Aqui agregamos el Rol de Administrador para el usuario Admin
                    if (customerUser is not null)
                    {

                        await userManager.AddToRoleAsync(customerUser, Constantes.RolUsuarioGoreuTramite);
                        var usuarioUnidadOrganica2 = new UsuarioUnidadOrganica
                        {

                            IdUsuario = customerUser.Id,
                            IdUnidadOrganica = unidadOrganica1.Id
                        };
                        context.Set<UsuarioUnidadOrganica>().Add(usuarioUnidadOrganica2);
                        await context.SaveChangesAsync();

                    }
                }
            }
            #endregion

            #region UsuarioTres

            if (await userManager.FindByEmailAsync("pp.llerenalima@gmail.com") is null)
            {
                adminUser2.IdPersona = persona3.Id;

                var result = await userManager.CreateAsync(adminUser2, "Piero*2025");
                if (result.Succeeded)
                {
                    // Obtenemos el registro del usuario
                    adminUser2 = await userManager.FindByEmailAsync(adminUser2.Email);
                    // Aqui agregamos el Rol de Administrador para el usuario Admin
                    if (adminUser2 is not null)
                    {

                        await userManager.AddToRoleAsync(adminUser2, role3.Name);
                        var usuarioUnidadOrganica3 = new UsuarioUnidadOrganica
                        {

                            IdUsuario = adminUser2.Id,
                            IdUnidadOrganica = unidadOrganica1.Id
                        };
                        context.Set<UsuarioUnidadOrganica>().Add(usuarioUnidadOrganica3);
                        await context.SaveChangesAsync();

                    }
                }
            }

            #endregion

        }

    }
}
