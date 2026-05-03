using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Configuración de Servicios

// 1. DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.AddHttpContextAccessor();

// 2. Configuración AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration);

// 3. Identity
builder.Services.AddIdentity<Usuario, Rol>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 4. JWT Authentication
//////////////////////////////////con audiencia///////////////////////////////////////
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTKey"] ??
//        throw new InvalidOperationException("JWT key not configured"));

//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = builder.Configuration["JWT:Issuer"],

//        ValidateAudience = true,
//        ValidAudiences = builder.Configuration.GetSection("JWT:Audiences").Get<string[]>(),

//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key)
//    };
//});
///////////////////////////sin audiencia/////////////////////////////////////////////////
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // esquema por defecto: JWT
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // si no hay auth -> usa JWT challenge
}).AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTKey"] 
        ?? throw new InvalidOperationException("JWT key not configured."));
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,               // no valida quién emitió el token
        ValidateAudience = false,             // no valida la audiencia
        ValidateLifetime = true,              // sí valida expiración
        ValidateIssuerSigningKey = true,      // sí valida la firma
        IssuerSigningKey = new SymmetricSecurityKey(key) // clave para validar la firma
    };
});

// 5. Autorización
builder.Services.AddAuthorization();

// 7. Repositorios (Acceso a Datos)
builder.Services.AddTransient<IAplicacionRepository, AplicacionRepository>();
builder.Services.AddTransient<IEntidadAplicacionRepository, EntidadAplicacionRepository>();
builder.Services.AddTransient<IEntidadRepository, EntidadRepository>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddTransient<IRolRepository, RolRepository>();
builder.Services.AddTransient<IUnidadOrganicaRepository, UnidadOrganicaRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddTransient<IUsuarioUnidadOrganicaRepository, UsuarioUnidadOrganicaRepository>();
builder.Services.AddTransient<ITipoDocumentoRepository, TipoDocumentoRepository>();
builder.Services.AddTransient<ICredencialReniecRepository, CredencialReniecRepository>();
builder.Services.AddTransient<IHistorialRepository, HistorialRepository>();
builder.Services.AddTransient<IMenuRolRepository, MenuRolRepository>();

// 8. Servicios (Lógica de Negocio)
builder.Services.AddTransient<IAplicacionService, AplicacionService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IEntidadAplicacionService, EntidadAplicacionService>();
builder.Services.AddTransient<IEntidadService, EntidadService>();
builder.Services.AddTransient<IMenuService, MenuService>();
builder.Services.AddTransient<IPersonaService, PersonaService>();
builder.Services.AddTransient<IRolService, RolService>();
builder.Services.AddTransient<IUnidadOrganicaService, UnidadOrganicaService>();
builder.Services.AddTransient<IUsuarioUnidadOrganicaService, UsuarioUnidadOrganicaService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<ITipoDocumentoService, TipoDocumentoService>();
builder.Services.AddTransient<ICredencialReniecService, CredencialReniecService>();
builder.Services.AddTransient<IHistorialService, HistorialService>();
builder.Services.AddTransient<IReniecApiClient, ReniecApiClient>();
builder.Services.AddTransient<IReniecService, ReniecService>();

builder.Services.AddTransient<IMenuRolService,MenuRolService>();



// 9. AutoMapper
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<AplicacionProfile>();
    config.AddProfile<MenuProfile>();
    config.AddProfile<PersonaProfile>();
    config.AddProfile<EntidadProfile>();
    config.AddProfile<RolProfile>();
    config.AddProfile<UsuarioProfile>();
    config.AddProfile<UserRoleProfile>();
    config.AddProfile<EntidadAplicacionProfile>();
    config.AddProfile<UnidadOrganicaProfile>();
    config.AddProfile<UsuarioUnidadOrganicaProfile>();
    config.AddProfile<TipoDocumentoProfile>();
    config.AddProfile<CredencialReniecProfile>();

    config.AddProfile<MenuRolProfile>();
    

});

// 10. Semilla de Datos
builder.Services.AddTransient<UserDataSeeder>();

// 6. CORS
//////////////////////////////////con audiencia///////////////////////////////////////
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowConfiguredOrigins", policyBuilder =>
//    {
//        var allowedOrigins = builder.Configuration.GetSection("JWT:Audiences").Get<string[]>() ??
//                             throw new InvalidOperationException("JWT Audiences not configured.");

//        if (allowedOrigins.Any())
//        {
//            policyBuilder.WithOrigins(allowedOrigins)
//                         .AllowAnyMethod()
//                         .AllowAnyHeader()
//                         .AllowCredentials()
//                         .WithExposedHeaders("totalrecordsquantity");
//        }
//        else
//        {
//            throw new InvalidOperationException("No se configuraron orígenes válidos en JWT:Audiences.");
//        }
//    });
//});
///////////////////////////sin audiencia/////////////////////////////////////////////////
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader().WithExposedHeaders(new string[] { "totalrecordsquantity" });
        policy.AllowAnyMethod();
    });
});

/////////////////////////////////////////////////////////////////////////////////////////////////

// 11. Controladores y HTTP Client
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// 12. Swagger
builder.Services.AddEndpointsApiExplorer();

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
var isProduction = builder.Environment.IsProduction();

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Goreu.API",
        Description = "Documentación de la API Goreu",
        Version = "v1"
    });

    config.IncludeXmlComments(xmlPath);

    // Solo en Producción se agrega el esquema de seguridad JWT en Swagger UI
    if (isProduction)
    {
        config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Ingresa tu token JWT así: Bearer {token}"
        });

        config.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }

});

#endregion

#region Pipeline HTTP

var app = builder.Build();


// ============================================================
// 🔐 SEGURIDAD SWAGGER - PASO 2: Habilitar Swagger en ambos ambientes
// Development → acceso libre en "/swagger", sin restricciones.
// Production  → acceso protegido en "/swagger", con login del PASO 4.
// ============================================================


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");

// ============================================================
// 🔐 SEGURIDAD SWAGGER - PASO 4: Orden correcto del Middleware
// UseAuthentication() DEBE ir antes que UseAuthorization().
// Sin este orden Identity no puede resolver el usuario
// y el middleware de Basic Auth del PASO 5 fallará.
// ============================================================
app.UseAuthentication();
app.UseAuthorization();

// ============================================================
// 🔐 SEGURIDAD SWAGGER - PASO 5: Middleware Basic Auth (solo en Producción)
// En Development este bloque se omite completamente → Swagger libre.
// En Production intercepta peticiones a "/swagger" y aplica este flujo:
//   1. El navegador muestra una ventana emergente de usuario/contraseña.
//   2. Las credenciales viajan en Base64 en el header "Authorization".
//   3. Se decodifican y validan contra la BD usando ASP.NET Identity.
//   4. Se verifica que el usuario tenga el rol "Developer".
//   5. ✅ Válido → accede a Swagger.
//   6. ❌ Inválido → el navegador vuelve a mostrar la ventana de login.
// ============================================================
if (app.Environment.IsProduction())
{
    app.Use(async (context, next) =>
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            // Sin header → el navegador muestra la ventana emergente de login
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic "))
            {
                context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Swagger - Solo Developers\"";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            // Decodificar credenciales Base64 → "usuario:contraseña"
            var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
            var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
            var separatorIndex = decodedCredentials.IndexOf(':');

            if (separatorIndex < 0)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }

            var username = decodedCredentials.Substring(0, separatorIndex);
            var password = decodedCredentials.Substring(separatorIndex + 1);

            // Validar usuario y contraseña contra la base de datos con Identity
            var userManager = context.RequestServices.GetRequiredService<UserManager<Usuario>>();
            var signInManager = context.RequestServices.GetRequiredService<SignInManager<Usuario>>();

            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                // Usuario no existe → volver a mostrar ventana de login
                context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Swagger - Solo Developers\"";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                // Contraseña incorrecta → volver a mostrar ventana de login
                context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Swagger - Solo Developers\"";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            // ============================================================
            // 🔐 SEGURIDAD SWAGGER - PASO 6: Verificación del Rol "Developer"
            // No basta autenticarse, el usuario también debe tener el rol
            // "Developer" asignado en la base de datos.
            // Si no tiene el rol → 403 Forbidden (no se vuelve a pedir login).
            // ============================================================
            var roles = await userManager.GetRolesAsync(user);

            if (!roles.Contains("Developer"))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Acceso denegado: Se requiere el rol Developer.");
                return;
            }

            // ✅ Credenciales válidas + rol Developer → acceso permitido a Swagger
        }

        await next();
    });
}

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Goreu.API Swagger");

    // ============================================================
    // 🔐 SEGURIDAD SWAGGER - PASO 3: Misma ruta "/swagger" en ambos ambientes
    // La protección NO depende de la ruta sino del middleware del PASO 4.
    // En Development → entra directo sin login.
    // En Production  → el middleware intercepta y pide credenciales.
    // ============================================================
    config.RoutePrefix = "swagger";
});


app.MapControllers();

await ApplyMigrationsAndSeedDataAsync(app);

app.Run();

#endregion

#region Método Semilla y Migraciones

static async Task ApplyMigrationsAndSeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (dbContext.Database.GetPendingMigrations().Any())
    {
        await dbContext.Database.MigrateAsync();
    }

    var userDataSeeder = scope.ServiceProvider.GetRequiredService<UserDataSeeder>();
    await userDataSeeder.SeedAsync();
}

#endregion
