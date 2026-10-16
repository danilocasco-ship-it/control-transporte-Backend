using API_CONTROL_TRANSPORTE.Extensions;
using Application.Proveedores;
using Application.Equipos;
using Application.Catalogos;
using Application.PrestamosEquipos;
using IntegracionLogin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Escucha en todas las interfaces para que sea accesible por IP desde la red
// (movil/PC) y no solo desde localhost.
// Si se publica detras de IIS, IIS controla el puerto 8097 y esto se ignora.
builder.WebHost.UseUrls("http://0.0.0.0:8097");

builder.Services.AddControllers();

// Autenticacion JWT. JwtService firma con HmacSha256 y Encoding.ASCII sobre
// Jwt:Key, asi que la validacion DEBE usar la misma codificacion: con UTF8 los
// tokens que emite el login no validarian.
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("Falta 'Jwt:Key' en la configuracion: la API no podria validar tokens.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
            ValidateIssuer = false,   // JwtService no emite issuer
            ValidateAudience = false, // JwtService no emite audience
            ValidateLifetime = true
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<ICatalogoEquipoService, CatalogoEquipoService>();
builder.Services.AddScoped<IPrestamoEquipoService, PrestamoEquipoService>();
builder.Services.AddLogging();

builder.Services.AddCustomServices();
builder.Services.AgregarIntegracionLogin(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "http://localhost:4201",
                "http://localhost:4203",
                "http://172.16.0.67",
                "http://172.16.0.67:4203")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Content-Disposition");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
// UseAuthentication debe ir antes de UseAuthorization: sin el, [Authorize] no
// tiene con que validar el token y toda peticion autenticada responde 401.
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
