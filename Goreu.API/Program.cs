using Goreu.Entities;
using Goreu.Persistence;
using Goreu.Repositories.Implementation;
using Goreu.Repositories.Implementation.Pide;
using Goreu.Repositories.Interface;
using Goreu.Repositories.Interface.Pide;
using Goreu.Services.Implementation;
using Goreu.Services.Implementation.Pide;
using Goreu.Services.Interface;
using Goreu.Services.Interface.Pide;
using Goreu.Services.Profiles;
using Goreu.Services.Profiles.Pide;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.Configure<AppSettings>(builder.Configuration);




//6.identity
////builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddIdentity<Usuario, IdentityRole>(polices =>
{
    polices.Password.RequireDigit = true;
    polices.Password.RequiredLength = 6;
    polices.User.RequireUniqueEmail = true;


})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//jwt

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTKey"] ??
        throw new InvalidOperationException("JWT key not configured"));
    x.TokenValidationParameters = new TokenValidationParameters
    {
        // ValidateIssuer = false,
        //ValidateAudience = false,
        //ValidateLifetime = true,
        //ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(key)

        ValidateIssuer = true, //Habilita la validación del emisor
        ValidIssuer = builder.Configuration["JWT:Issuer"], //Define el emisor del token
        ValidateAudience = true, //Habilita la validación de la audiencia
        ValidAudiences = builder.Configuration.GetSection("JWT:Audiences").Get<string[]>(), //Define la audiencia esperada
        ValidateLifetime = true, //Asegúrate de que el token no esté expirado
        ValidateIssuerSigningKey = true, //Valida la clave de firma
        IssuerSigningKey = new SymmetricSecurityKey(key)


    };
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins", policyBuilder =>
    {
        //Obtenemos las URL de los clientes válidos desde appsettings
        var allowedOrigins = builder.Configuration.GetSection("JWT:Audiences").Get<string[]>() ??
                             throw new InvalidOperationException("JWT Audiences not configured.");

        if (allowedOrigins.Any())
        {
            policyBuilder.WithOrigins(allowedOrigins) //Permite solo los orígenes configurados
                .AllowAnyMethod() //Puedes ajustar los métodos según tu necesidad
                .AllowAnyHeader() //Permitir solo los encabezados necesarios si aplica
                .AllowCredentials(); //Anotacion pra produccion

        }
        else
        {
            throw new InvalidOperationException("No se configuraron orígenes válidos en JWT:Audiences.");
        }
    }


    );
});
builder.Services.AddAuthorization();

builder.Services.AddTransient<IAplicacionRepository, AplicacionRepository>();
builder.Services.AddTransient<IEntidadAplicacionRepository,EntidadAplicacionRepository>();
builder.Services.AddTransient<IEntidadRepository,EntidadRepository>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddTransient<IRolRepository,RolRepository>();
builder.Services.AddTransient<IUnidadOrganicaRepository,UnidadOrganicaRepository>();
builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddTransient<IUsuarioUnidadOrganicaRepository,UsuarioUnidadOrganicaRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ITipoDocumentoRepository, TipoDocumentoRepository>();
builder.Services.AddTransient<ICredencialReniecRepository,CredencialReniecRepository>();
builder.Services.AddTransient<IHistorialRepository, HistorialRepository>();



builder.Services.AddTransient<IAplicacionService, AplicacionService>();
builder.Services.AddTransient<IEmailService,EmailService>();
builder.Services.AddTransient<IEntidadAplicacionService,EntidadAplicacionService>();
builder.Services.AddTransient<IEntidadService,EntidadService>();
builder.Services.AddTransient<IMenuService,MenuService>();
builder.Services.AddTransient<IPersonaService,PersonaService>();
builder.Services.AddTransient<IRolService, RolService>();
builder.Services.AddTransient<IUnidadOrganicaService,UnidadOrganicaService>();
builder.Services.AddTransient<IUsuarioUnidadOrganicaService,UsuarioUnidadOrganicaService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITipoDocumentoService,TipoDocumentoService>();
builder.Services.AddTransient<ICredencialReniecService, CredencialReniecService>();
builder.Services.AddTransient<IHistorialService, HistorialService>();





//3.register mapper
builder.Services.AddAutoMapper(config =>
{
    //configuring the mapping perfiles
    
    config.AddProfile<AplicacionProfile>();
    config.AddProfile<MenuProfile>();
    config.AddProfile<PersonaProfile>();
    config.AddProfile<EntidadProfile>();
    config.AddProfile<RolProfile>();
    config.AddProfile<EntidadAplicacionProfile>();
    config.AddProfile<UnidadOrganicaProfile>();
    config.AddProfile<UsuarioUnidadOrganicaProfile>();
    config.AddProfile<TipoDocumentoProfile>();
    config.AddProfile<CredencialReniecProfile>();

});

///data semilla parte 1
builder.Services.AddTransient<UserDataSeeder>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowConfiguredOrigins", policyBuilder =>
    {
        //Obtenemos las URL de los clientes válidos desde appsettings
        var allowedOrigins = builder.Configuration.GetSection("JWT:Audiences").Get<string[]>() ??
                             throw new InvalidOperationException("JWT Audiences not configured.");

        if (allowedOrigins.Any())
        {
            policyBuilder.WithOrigins(allowedOrigins) //Permite solo los orígenes configurados
                .AllowAnyMethod() //Puedes ajustar los métodos según tu necesidad
                .AllowAnyHeader() //Permitir solo los encabezados necesarios si aplica
                .AllowCredentials(); //Anotacion pra produccion

        }
        else
        {
            throw new InvalidOperationException("No se configuraron orígenes válidos en JWT:Audiences.");
        }
    }


    );
});

builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Advanced Swagger configuration
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Goreu.API",
            Description = "This is the documentation for my api in Goreu.",
            Version = "v1"
        });
    config.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowConfiguredOrigins");

app.MapControllers();

// Aplicar migraciones y sembrar datos (asíncronamente) data semilla parte 2
await ApplyMigrationsAndSeedDataAsync(app);

app.Run();


///data semilla parte 3
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
