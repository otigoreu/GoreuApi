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

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Goreu.API",
        Description = "Documentación de la API Goreu",
        Version = "v1"
    });

    config.IncludeXmlComments(xmlPath);
});

#endregion

#region Pipeline HTTP

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "Goreu.API Swagger");
        config.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseRouting();

//app.UseCors("AllowConfiguredOrigins"); 
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

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
